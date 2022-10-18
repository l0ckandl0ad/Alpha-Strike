public interface IRoutineCarrierOpsSettings
{
    float Range { get; }
    float SearchFrequencyInMinutes { get; }

    bool ZeroDegrees { get; }
    bool FortyFiveDegrees { get; }
    bool NinetyDegrees { get; }
    bool OneThirtyFiveDegrees { get; }
    bool OneEightyDegrees { get; }
    bool TwoTwentyFiveDegrees { get; }
    bool TwoSeventyDegrees { get; }
    bool ThreeFifteenDegrees { get; }

    bool Shadow { get; }

    float[,] GetSearchSectors();

    void SetRange(float range);
    void SetSearchFrequencyInMinutes(float searchFrequencyInMinutes);

    void SetZeroDegrees(bool value);
    void SetFortyFiveDegrees(bool value);
    void SetNinetyDegrees(bool value);
    void SetOneThirtyFiveDegrees(bool value);
    void SetOneEightyDegrees(bool value);
    void SetTwoTwentyFiveDegrees(bool value);
    void SetTwoSeventyDegrees(bool value);
    void SetThreeFifteenDegrees(bool value);

    void SetShadow(bool value);
}