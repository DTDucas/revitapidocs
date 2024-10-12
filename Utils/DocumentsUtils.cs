Document Utils

TESTED REVIT API: 2024

Author: DTDucas aka DUONG TRAN | https://github.com/DTDucas

Shared on www.revitapidocs.com
For more information visit http://github.com/gtalarico/revitapidocs
License: http://github.com/gtalarico/revitapidocs/blob/master/LICENSE.md



    public static class DocumentUtils
{
    public static List<Element> GetAllInstancesOfFamily(this Document doc, string familyName)
    {
        return new FilteredElementCollector(doc)
            .WhereElementIsNotElementType()
            .Where(e => e.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM)?.AsValueString() == familyName)
            .ToList();
    }

    public static List<Element> GetAllElementsOfCategory(this Document doc, BuiltInCategory category)
    {
        return new FilteredElementCollector(doc)
            .OfCategory(category)
            .WhereElementIsNotElementType()
            .ToList();
    }

    public static FamilySymbol GetFamilySymbolByName(this Document doc, string familyName, string typeName)
    {
        return new FilteredElementCollector(doc)
            .OfClass(typeof(FamilySymbol))
            .Cast<FamilySymbol>()
            .FirstOrDefault(fs => fs.FamilyName == familyName && fs.Name == typeName);
    }

    public static void DeleteElements(this Document doc, ICollection<ElementId> elementIds)
    {
        using (Transaction trans = new Transaction(doc, "Delete Elements"))
        {
            trans.Start();
            doc.Delete(elementIds);
            trans.Commit();
        }
    }

    public static Element CreateFamilyInstance(this Document doc, FamilySymbol symbol, XYZ location, Level level)
    {
        using (Transaction trans = new Transaction(doc, "Create Family Instance"))
        {
            trans.Start();
            if (!symbol.IsActive)
            {
                symbol.Activate();
            }
            Element instance = doc.Create.NewFamilyInstance(location, symbol, level, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            trans.Commit();
            return instance;
        }
    }

    public static ViewSheet CreateSheet(this Document doc, string sheetNumber, string sheetName)
    {
        ViewSheet sheet = null;
        using (Transaction trans = new Transaction(doc, "Create Sheet"))
        {
            trans.Start();
            sheet = ViewSheet.Create(doc, ElementId.InvalidElementId);
            sheet.SheetNumber = sheetNumber;
            sheet.Name = sheetName;
            trans.Commit();
        }
        return sheet;
    }

    public static Parameter GetParameterByName(this Element element, string paramName)
    {
        return element.GetParameters(paramName).FirstOrDefault();
    }

    public static void SetParameterValue(this Element element, string paramName, object value)
    {
        Parameter param = element.GetParameterByName(paramName);
        if (param is { IsReadOnly: false })
        {
            using (Transaction trans = new Transaction(Document, "Set Parameter Value"))
            {
                trans.Start();
                if (value is double d)
                    param.Set(d);
                else if (value is int i)
                    param.Set(i);
                else if (value is string s)
                    param.Set(s);
                else if (value is ElementId id)
                    param.Set(id);
                trans.Commit();
            }
        }
    }

    public static List<View> GetAllViews(this Document doc)
    {
        return new FilteredElementCollector(doc)
            .OfClass(typeof(View))
            .Cast<View>()
            .Where(v => !v.IsTemplate)
            .ToList();
    }
}