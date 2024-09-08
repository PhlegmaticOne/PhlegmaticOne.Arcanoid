﻿using System;

namespace Game.Field
{
    public readonly struct FieldPosition : IEquatable<FieldPosition>
    {
        public static FieldPosition None => new FieldPosition(-1, -1);
        public static FieldPosition RightDirection => new FieldPosition(0, 1);
        public static FieldPosition LeftDirection => new FieldPosition(0, -1);
        public static FieldPosition UpDirection => new FieldPosition(-1, 0);
        public static FieldPosition DownDirection => new FieldPosition(1, 0);
        public FieldPosition(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; }
        public int Col { get; }

        public FieldPosition Up(int times = 1) => new FieldPosition(Row - times, Col);
        public FieldPosition Down(int times = 1) => new FieldPosition(Row + times, Col);
        public FieldPosition Left(int times = 1) => new FieldPosition(Row, Col - times);
        public FieldPosition Right(int times = 1) => new FieldPosition(Row, Col + times);
        public FieldPosition LeftUp(int times = 1) => new FieldPosition(Row - times, Col - times);
        public FieldPosition RightUp(int times = 1) => new FieldPosition(Row - times, Col + times);
        public FieldPosition LeftDown(int times = 1) => new FieldPosition(Row + times, Col - times);
        public FieldPosition RightDown(int times = 1) => new FieldPosition(Row + times, Col + times);

        public static bool operator ==(FieldPosition a, FieldPosition b) => a.Equals(b);

        public static bool operator !=(FieldPosition a, FieldPosition b) => !(a == b);

        public static FieldPosition operator +(FieldPosition a, FieldPosition b) =>
            new FieldPosition(a.Row + b.Row, a.Col + b.Col);
        public static FieldPosition operator -(FieldPosition a, FieldPosition b) => 
            new FieldPosition(a.Row - b.Row, a.Col - b.Col);
        public bool Equals(FieldPosition other) => Row == other.Row && Col == other.Col;

        public override bool Equals(object obj) => obj is FieldPosition other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Col;
            }
        }

        public override string ToString() => $"Row: {Row}, Col: {Col}";
    }
}