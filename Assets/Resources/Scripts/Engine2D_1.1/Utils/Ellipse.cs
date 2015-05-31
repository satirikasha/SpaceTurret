namespace Engine.Utils {
  using Engine.Interface;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using UnityEngine;

  /// <summary>
  /// Элипс
  /// </summary>
  /// Формула: ((x - Center.x) / (Width / 2)) ^ 2 + ((y - Center.y) / (Height / 2)) ^ 2 = 1
  public class Ellipse: IContainer {

    public Vector2 Center { get; set; }
    public float Width { get; private set; }
    public float Height { get; private set; }

    private float PrecisionCircleRadius;

    public Ellipse(Vector2 center, float width, float height) {
      Center = center;
      Width = width;
      Height = height;
      PrecisionCircleRadius = (Height + Width) / 2;
    }

    public Ellipse(float x, float y, float width, float height) {
      Center = new Vector2(x, y);
      Width = width;
      Height = height;
      PrecisionCircleRadius = (Height + Width) / 2;
    }

    public void SetWidth(float width) {
      Width = width;
      PrecisionCircleRadius = (Height + Width) / 2;
    }

    public void SetHeight(float height) {
      Height = height;
      PrecisionCircleRadius = (Height + Width) / 2;
    }

    public bool Contains(Vector2 point) {
      return ((point.x - Center.x) / (Width / 2)).deg2() + ((point.y - Center.y) / (Height / 2)).deg2() <= 1;
    }

    public bool PrecicelyContains(Vector2 point) {
      return (point - Center).magnitude <= PrecisionCircleRadius;
    }

    public Vector2 GetRandomPoint() {
      return GetRandomDelta() + Center;
    }

    public Vector2 GetRandomDelta() {
      var X = Random.Range(-Width / 2, Width / 2);
      var Ymax = ((Height / 2).deg2() - (Height / 2).deg2() * X.deg2() / (Width / 2).deg2()).sqr2();
      var Y = Random.Range(-Ymax, Ymax);
      return new Vector2(X, Y);
    }

    /// <summary>
    /// Находит ближайшую точку, лежащую на прямой между центром эллипса и исходной точкой
    /// </summary>
    public Vector2 GetRadialPoint(Vector2 point) {
      //прямая kx+m=y
      var k = (point.y - Center.y) / (point.x - Center.x);
      var m = Center.y - k * Center.x;

      //формула эллипса сводится к квадратному уравнению ax^2 + bx + c = 0
      var rx = Width / 2;
      var ry = Height / 2;
      var a = 1 / rx.deg2() + k.deg2() / ry.deg2();
      var b = 2 * k * (m - Center.y) / ry.deg2() - 2 * Center.x / rx.deg2();
      var c = Center.x.deg2() / rx.deg2() + (m - Center.y).deg2() / ry.deg2() - 1;
      var D = b.deg2() - 4 * a * c;
      if(D < 0)
        return Vector2.zero;
      var x1 = (-b - D.sqr2()) / (2 * a);
      var x2 = (-b + D.sqr2()) / (2 * a);
      var y1 = k * x1 + m;
      var y2 = k * x2 + m;
      var v1 = new Vector2(x1, y1);
      var v2 = new Vector2(x2, y2);
      return (point - v1).magnitude > (point - v2).magnitude ? v2 : v1;
    }
  }
}