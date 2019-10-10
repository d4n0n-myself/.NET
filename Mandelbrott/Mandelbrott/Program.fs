open Microsoft.FSharp.Math
open System
open System.Drawing
open System.Windows.Forms

// границы комлпексных чисел
let cMax = complex 1.0 1.0
let cMin = complex -1.0 -1.0

// принадлежность 
let rec inMandelbrot (z, c, i, count) =
    if (cMin < z) && (z < cMax) && (count < i) then
        inMandelbrot (((z * z) + c), c, i, (count + 1))
    else count

// перенос стандартной плоскости на новую плоскость
let scalingFactor s = s * 1.0 / 200.0

let mapPlane (x, y, s, diffX, diffY) =
    let fx = ((float x) * scalingFactor s) + diffX
    let fy = ((float y) * scalingFactor s) + diffY
    complex fx fy

// раскрасить точки, не относящиеся к множеству
let addColor number =
    let random = new Random()
    let r = (random.Next(16) * number) % 255
    let g = (random.Next(16) * number) % 255
    let b = (random.Next(16) * number) % 255
    Color.FromArgb(r, g, b)

let createImage (s, navX, navY, iterations) =
    let bitmap = new Bitmap(400, 400)
    for x = 0 to bitmap.Width - 1 do
        for y = 0 to bitmap.Height - 1 do
            let count = inMandelbrot (Complex.Zero, (mapPlane (x, y, s, navX, navY)), iterations, 0)
            if count = iterations then
                bitmap.SetPixel(x, y, Color.Black)
            else
                bitmap.SetPixel(x, y, addColor (count))
    let form = new Form()
    form.Paint.Add(fun e -> e.Graphics.DrawImage(bitmap, 0, 0))
    form

[<STAThread>]
do Application.Run(createImage (1.5, -1.5, -1.5, 20))