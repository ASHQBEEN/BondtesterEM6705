namespace ashqTech
{
    public enum AxisIO : uint
    {
        AX_MOTION_IO_RDY = 1u,
        AX_MOTION_IO_ALM = 2u,
        AX_MOTION_IO_LMTP = 4u,
        AX_MOTION_IO_LMTN = 8u,
        AX_MOTION_IO_ORG = 0x10u,
        AX_MOTION_IO_DIR = 0x20u,
        AX_MOTION_IO_EMG = 0x40u,
        AX_MOTION_IO_PCS = 0x80u,
        AX_MOTION_IO_ERC = 0x100u,
        AX_MOTION_IO_EZ = 0x200u,
        AX_MOTION_IO_CLR = 0x400u,
        AX_MOTION_IO_LTC = 0x800u,
        AX_MOTION_IO_SD = 0x1000u,
        AX_MOTION_IO_INP = 0x2000u,
        AX_MOTION_IO_SVON = 0x4000u,
        AX_MOTION_IO_ALRM = 0x8000u,
        AX_MOTION_IO_SLMTP = 0x10000u,
        AX_MOTION_IO_SLMTN = 0x20000u,
        AX_MOTION_IO_CMP = 0x40000u,
        AX_MOTION_IO_CAMDO = 0x80000u,
        AX_MOTION_IO_MAXTORLMT = 0x100000u
    }
}
