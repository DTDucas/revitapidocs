Column Selection Filter

TESTED REVIT API: 2024

Author: DTDucas aka DUONG TRAN | https://github.com/DTDucas

Shared on www.revitapidocs.com
For more information visit http://github.com/gtalarico/revitapidocs
License: http://github.com/gtalarico/revitapidocs/blob/master/LICENSE.md


public class ColumnSelectionFilter : ISelectionFilter
{
    public bool AllowElement(Element elem)
    {
        return elem.Category?.Id.Value == -2001330;
    }

    public bool AllowReference(Reference reference, XYZ position)
    {
        return false;
    }
}