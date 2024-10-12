Geometry Utils

TESTED REVIT API: 2024

Author: DTDucas aka DUONG TRAN | https://github.com/DTDucas

Shared on www.revitapidocs.com
For more information visit http://github.com/gtalarico/revitapidocs
License: http://github.com/gtalarico/revitapidocs/blob/master/LICENSE.md



public static class GeometryUtils
{
    public static XYZ GetMidpoint(this Line line)
    {
        return (line.GetEndPoint(0) + line.GetEndPoint(1)) * 0.5;
    }

    public static double DistanceTo(this XYZ point1, XYZ point2)
    {
        return point1.DistanceTo(point2);
    }

    public static XYZ ProjectOnto(this XYZ vector, XYZ ontoVector)
    {
        if (!ontoVector.IsUnitLength())
        {
            ontoVector = ontoVector.Normalize();
        }
        return ontoVector * vector.DotProduct(ontoVector);
    }

    public static bool IsZero(this XYZ vector, double tolerance = 1.0e-9)
    {
        return vector.GetLength() < tolerance;
    }

    public static Solid CreateSolidFromBoundingBox(this BoundingBoxXYZ bb)
    {
        var pt0 = bb.Min;
        var pt1 = bb.Max;
        var height = pt1.Z - pt0.Z;
        var width = pt1.X - pt0.X;
        var depth = pt1.Y - pt0.Y;

        var profile = new List<XYZ>
    {
        pt0,
        new XYZ(pt1.X, pt0.Y, pt0.Z),
        new XYZ(pt1.X, pt1.Y, pt0.Z),
        new XYZ(pt0.X, pt1.Y, pt0.Z),
        pt0
    };

        var curveLoop = CurveLoop.Create(profile.Select(p => Line.CreateBound(p, p) as Curve).ToList());
        return GeometryCreationUtilities.CreateExtrusionGeometry(new List<CurveLoop> { curveLoop }, XYZ.BasisZ, height);
    }

    public static IEnumerable<Face> GetFaces(this Solid solid)
    {
        return solid.Faces.Cast<Face>();
    }

    public static IEnumerable<Edge> GetEdges(this Solid solid)
    {
        return solid.Edges.Cast<Edge>();
    }

    public static XYZ TransformPoint(this Transform transform, XYZ point)
    {
        return transform.OfPoint(point);
    }

    public static XYZ TransformVector(this Transform transform, XYZ vector)
    {
        return transform.OfVector(vector);
    }
}