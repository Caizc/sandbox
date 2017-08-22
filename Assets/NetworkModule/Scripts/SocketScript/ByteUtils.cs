using System;

/// <summary>
///     字节数组工具类
/// </summary>
public static class ByteUtils
{
    /// <summary>
    ///     byte[] to int
    /// </summary>
    /// <param name="bt"></param>
    /// <param name="starIndex"></param>
    /// <returns></returns>
    public static int ByteArray2Int(byte[] bt, int starIndex)
    {
        var num = BitConverter.ToInt32(bt, starIndex);
        return num;
    }

    /// <summary>
    ///     int to byte[]
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static byte[] Int2ByteArray(int num)
    {
        var bt = BitConverter.GetBytes(num);
        return bt;
    }

    /// <summary>
    ///     byte[] to float
    /// </summary>
    /// <param name="bt"></param>
    /// <param name="starIndex"></param>
    /// <returns></returns>
    public static float ByteArray2Float(byte[] bt, int starIndex)
    {
        var f = BitConverter.ToSingle(bt, starIndex);
        return f;
    }

    /// <summary>
    ///     float to byte[]
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static byte[] Float2ByteArray(float f)
    {
        var bt = BitConverter.GetBytes(f);
        return bt;
    }
}