public override void FloodFill(Bitmap bmp, Point pt)
{
    //timeGetTime() is used instead of the 
    //performance ctr, for Win98/ME compatibility
    int ctr=timeGetTime(); 
    
    //get the color's int value, and convert it from 
    //RGBA to BGRA format (as GDI+ uses BGRA)
    m_fillcolor=ColorTranslator.ToWin32(m_fillcolorcolor);
    m_fillcolor=BGRA(GetB(m_fillcolor),
        GetG(m_fillcolor),GetR(m_fillcolor),GetA(m_fillcolor));
    
    //get the bits
    BitmapData bmpData=bmp.LockBits(
            new Rectangle(0,0,bmp.Width,bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format32bppArgb);
    System.IntPtr Scan0 = bmpData.Scan0;
    
    unsafe
    {
        //resolve pointer
        byte * scan0=(byte *)(void *)Scan0;
        //get the starting color
        //[loc += Y offset + X offset]
        int loc=CoordsToIndex(pt.X,pt.Y,bmpData.Stride);
        int color= *((int*)(scan0+loc));
        
        //create the array of bools that indicates whether each pixel
        //has been checked.  
        //(Should be bitfield, but C# doesn't support bitfields.)
        PixelsChecked=new bool[bmpData.Width+1,bmpData.Height+1];
        
        //do the first call to the loop
        switch(m_FillStyle)
        {
            case FloodFillStyle.Linear :
                if(m_FillDiagonal)
                {
                        LinearFloodFill8(scan0,pt.X,pt.Y,
                            new Size(bmpData.Width,bmpData.Height),
                            bmpData.Stride,
                            (byte*)&color);
                }else{
                        LinearFloodFill4(scan0,pt.X,pt.Y,
                            new Size(bmpData.Width,bmpData.Height),
                            bmpData.Stride,
                            (byte*)&color);
                }
                break;
            case FloodFillStyle.Queue :
                QueueFloodFill(scan0,pt.X,pt.Y,
                    new Size(bmpData.Width,bmpData.Height),
                    bmpData.Stride,
                    (byte*)&color);
                break;
            case FloodFillStyle.Recursive :
                if(m_FillDiagonal)
                {
                        RecursiveFloodFill8(scan0,pt.X,pt.Y,
                            new Size(bmpData.Width,bmpData.Height),
                            bmpData.Stride,
                            (byte*)&color);
                }else{
                        RecursiveFloodFill4(scan0,pt.X,pt.Y,
                            new Size(bmpData.Width,bmpData.Height),
                            bmpData.Stride,
                            (byte*)&color);
                }
                break;
        }
    }
    
    bmp.UnlockBits(bmpData);
        
    m_TimeBenchmark=timeGetTime()-ctr;
        
}



unsafe void LinearFloodFill4( byte* scan0, int x, int y,Size bmpsize,
                                int stride, byte* startcolor)
{
        
    //offset the pointer to the point passed in
    int* p=(int*) (scan0+(CoordsToIndex(x,y, stride)));
    
    
    //FIND LEFT EDGE OF COLOR AREA
    int LFillLoc=x; //the location to check/fill on the left
    int* ptr=p; //the pointer to the current location
    while(true)
    {
        ptr[0]=m_fillcolor;      //fill with the color
        PixelsChecked[LFillLoc,y]=true;
        LFillLoc--;               //de-increment counter
        ptr-=1;                      //de-increment pointer
        if(LFillLoc<=0 || 
            !CheckPixel((byte*)ptr,startcolor) ||  
            (PixelsChecked[LFillLoc,y]))
                //exit loop if we're at edge of bitmap or color area
                break;
        
    }
    LFillLoc++;
    
    //FIND RIGHT EDGE OF COLOR AREA
    int RFillLoc=x; //the location to check/fill on the left
    ptr=p;
    while(true)
    {
        ptr[0]=m_fillcolor; //fill with the color
        PixelsChecked[RFillLoc,y]=true;
        RFillLoc++;          //increment counter
        ptr+=1;                 //increment pointer
        if(RFillLoc>=bmpsize.Width || 
            !CheckPixel((byte*)ptr,startcolor) ||  
            (PixelsChecked[RFillLoc,y]))
                //exit loop if we're at edge of bitmap or color area
                break;
        
    }
    RFillLoc--;
    
    
    //START THE LOOP UPWARDS AND DOWNWARDS            
    ptr=(int*)(scan0+CoordsToIndex(LFillLoc,y,stride));
    for(int i=LFillLoc;i<=RFillLoc;i++)
    {
      //START LOOP UPWARDS
      //if we're not above the top of the bitmap 
      //and the pixel above this one is within the color tolerance
      if(y>0 && 
       CheckPixel((byte*)(scan0+CoordsToIndex(i,y-1,stride)),startcolor) && 
       (!(PixelsChecked[i,y-1])))
           LinearFloodFill4(scan0, i,y-1,bmpsize,stride,startcolor);

      //START LOOP DOWNWARDS
      if(y<(bmpsize.Height-1) && 
      CheckPixel((byte*)(scan0+CoordsToIndex(i,y+1,stride)),startcolor) && 
      (!(PixelsChecked[i,y+1])))
           LinearFloodFill4(scan0, i,y+1,bmpsize,stride,startcolor);
      ptr+=1;
    }
        
}

///<SUMMARY>Sees if a pixel is within the color tolerance range.</SUMMARY>
//px - a pointer to the pixel to check
//startcolor - a pointer to the color of the pixel we started at
unsafe bool CheckPixel(byte* px, byte* startcolor)
{
    bool ret=true;
    for(byte i=0;i<3;i++)
            ret&= (px[i]>= (startcolor[i]-m_Tolerance[i])) &&
                    px[i] <= (startcolor[i]+m_Tolerance[i]);            
    return ret;
}