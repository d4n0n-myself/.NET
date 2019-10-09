open Microsoft.FSharp.Math
open System
open System.Drawing
open System.Windows.Forms

// границы комлпексных чисел
let cMax = complex 1.0 1.0
let cMin = complex -1.0 -1.0

// принадлежность 
let rec isInMandelbrotSet (z, c, iter, count) =
    if (cMin < z) && (z < cMax) && (count < iter) then
        isInMandelbrotSet (((z * z) + c), c, iter, (count + 1))
    else count

// отображение стандартной плоскости на новую плоскость
let scalingFactor s = s * 1.0 / 200.0

let mapPlane (x, y, s, mx, my) =
    let fx = ((float x) * scalingFactor s) + mx
    let fy = ((float y) * scalingFactor s) + my
    complex fx fy

// раскрасить точки, не относящиеся к множеству
let colorize c =
    let random = new Random()
    let r = (random.Next(16) * c) % 255
    let g = (random.Next(16) * c) % 255
    let b = (random.Next(16) * c) % 255
    Color.FromArgb(r, g, b)

let createImage (s, mx, my, iter) =
    let image = new Bitmap(400, 400)
    for x = 0 to image.Width - 1 do
        for y = 0 to image.Height - 1 do
            let count = isInMandelbrotSet (Complex.Zero, (mapPlane (x, y, s, mx, my)), iter, 0)
            if count = iter then
                image.SetPixel(x, y, Color.Black)
            else
                image.SetPixel(x, y, colorize (count))
    let form = new Form()
    form.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
    form

[<STAThread>]
do Application.Run(createImage (1.5, -1.5, -1.5, 20))