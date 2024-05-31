# Simple Vector Graphic Viewer

- Project is finished and it took totally 25 hours. I coded it respectfully to Clean Code rules. 
- First, I was thinking to use MVVM but then I just decided it's really not necessary to do it. 
- So, I changed my pattern to something similar to MVC. You can consider the Statics folder as the Control folder.

- You will see 3 Menu items when you run the program:

## Menu Items

### File
- **Open**: This will open an XML or JSON file.
- **Close**: If there is any file opened, this will close it.
- **Exit**: This will close the program.

### View
- **Show Axis Lines**: It contains a CheckBox, by checking or unchecking you can show the axis.
- **Scale In**: Must be enabled it BEFORE opening a file. If you enable it, it will scale in and show objects as much as possible.
- **Minimize**: Minimizes the window.
- **Maximize**: Maximizes the window.

### About
- **Dev's LinkedIn Page**: Will open my LinkedIn Page in your default browser.
- **Dev's GitHub Page**: Will open my GitHub Page in your default browser.
- **Dev's Xing Page**: Will open my Xing Page in your default browser.

## Possible Shape Types
- Line
- Circle
- Triangle
- Ellipse
- Rectangle
- Polygon (Unlimited Vertices)

### Ellipse
- I coded Ellipse and Circle in the same class. So, if you give 'width' and 'height' instead of 'radius',
- it will create an Ellipse. But type also has to be "ellipse". Here is a simple JSON input -of ellipse:
```json
{
    "type": "ellipse",
    "center": "14; 32",
    "height": 12.0,
    "width": 6.0,
    "filled": false,
    "color": "127; 255; 0; 0"
}
```
### Rectangle
- Rectangle is coded as a Path object just like Triangle. Just add a 'd' vertice and change type to 'Rectangle'.
- Here is a simple JSON input for rectangle:
```json
{
    "type": "rectangle",
    "a": "-47; 110",
    "b": "125; 120,3",
    "c": "125; 34",
    "d": "-60; 54",
    "filled": true,
    "color": "127; 255; 0; 255"
}
```
### Polygon
- For Polygon just add all the vertices in alphabetical order. (Because example input was like this, but I would prefer numbers or list definitions.) 
- There is no limitation on the count of vertices but you have to double-check if the vertices are sorted in clockwise or counterclockwise order. 
- It will not cause any errors but it can cause wrong calculation of area. Because I am using the Shoelace formula from Gauss to calculate the area. 
- As far as the paper says I can assume that input data is always valid, I am not sorting it and not limiting the user. Here is a simple JSON input of polygon:

```json
    {
        "type": "polygon", 
        "a": "-150; -150", 
        "b": "-130; -130", 
        "c": "-130; -110", 
        "d": "-60; -110", 
        "e": "-60; -150", 
        "f": "-120; -150,3", 
        "g": "-150; -140", 
        "filled": true, 
        "color": "127; 255; 0; 255" 
    }
```