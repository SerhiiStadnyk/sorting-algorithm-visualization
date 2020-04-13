using System;

public class PathAttribute : Attribute
{
    public string path;
    public PathAttribute(string path)
    {
        this.path = path;
    }
}