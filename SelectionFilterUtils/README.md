# Revit Selection Filters

This repository contains a collection of Revit Selection Filters to help you easily select specific types of elements in your Revit projects.

## Tested Revit API Version
2024

## Author
DTDucas aka DUONG TRAN | [GitHub Profile](https://github.com/DTDucas)

## Available Filters

Currently, this repository includes the following selection filters:

- `WallSelectionFilter`: For selecting wall elements

## How to Use

### 1. Add the Filter Class to Your Project

First, add the desired filter class to your Revit API project. For example, to use the `WallSelectionFilter`:

```csharp
public class WallSelectionFilter : ISelectionFilter
{
    public bool AllowElement(Element elem)
    {
        return elem.Category?.Id.IntegerValue == -2000011;
    }

    public bool AllowReference(Reference reference, XYZ position)
    {
        return false;
    }
}
```

### 2. Implement in Your Revit Command

Here's an example of how to use the `WallSelectionFilter` in a Revit command:

```csharp
[Transaction(TransactionMode.Manual)]
public class SelectWallsCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIApplication uiapp = commandData.Application;
        UIDocument uidoc = uiapp.ActiveUIDocument;
        Document doc = uidoc.Document;

        try
        {
            // Create an instance of the WallSelectionFilter
            WallSelectionFilter wallFilter = new WallSelectionFilter();

            // Prompt the user to select walls
            IList<Reference> selectedReferences = uidoc.Selection.PickObjects(ObjectType.Element, wallFilter, "Select walls");

            // Process the selected walls
            foreach (Reference reference in selectedReferences)
            {
                Element wall = doc.GetElement(reference);
                // Do something with the wall element
                TaskDialog.Show("Selected Wall", $"Wall ID: {wall.Id}");
            }

            return Result.Succeeded;
        }
        catch (Autodesk.Revit.Exceptions.OperationCanceledException)
        {
            // User canceled the selection
            return Result.Cancelled;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return Result.Failed;
        }
    }
}
```

### 3. Register and Run Your Command

Make sure to properly register your command in your Revit add-in manifest file, and then you can run it from within Revit.

## Notes

- The integer value `-2000011` is specific to the Wall category in Revit 2024. If you're using a different version of Revit, you may need to verify and update this value.
- This filter only allows the selection of elements and not references. If you need to select references, modify the `AllowReference` method accordingly.

## Contributing

Feel free to contribute to this project by adding more filters or improving existing ones. Please submit a pull request with your changes.

## License

This project is shared under the license specified at [http://github.com/gtalarico/revitapidocs/blob/master/LICENSE.md](http://github.com/gtalarico/revitapidocs/blob/master/LICENSE.md)

## Acknowledgements

This code is shared on [www.revitapidocs.com](http://www.revitapidocs.com). For more information, visit [http://github.com/gtalarico/revitapidocs](http://github.com/gtalarico/revitapidocs).