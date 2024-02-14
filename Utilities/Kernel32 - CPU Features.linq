<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

// https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-isprocessorfeaturepresent
//
// Lists the CPU features your CPU does or does not have.

void Main()
{
	IsUserAnAdmin().Dump ("We have administrative elevation");
	
	"".Dump();
	
	var features = new List<CpuFeature>();

	foreach (ProcessorFeature feature in System.Enum.GetValues(typeof(ProcessorFeature)))
	{
		var item = new CpuFeature(){FeatureCode = feature
			, FeatureDescription = feature.GetDescription()
			, HasFeature = IsProcessorFeaturePresent(feature)};
			
		features.Add(item);
	}
	
	features.Where(f => f.HasFeature == true).Dump("Features your CPU has");
	
	"".Dump();
	
	features.Where(f => f.HasFeature == false).Dump("Missing features");
}

[DllImport ("shell32.dll", EntryPoint = "#680")]
public static extern bool IsUserAnAdmin ();


[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
public static extern bool IsProcessorFeaturePresent(ProcessorFeature processorFeature);

public enum ProcessorFeature : uint
{
	[Description("On a Pentium, a floating-point precision error can occur in rare circumstances.")]
	PF_FLOATING_POINT_PRECISION_ERRATA = 0,

	[Description("Floating-point operations are emulated using a software emulator.")]
	PF_FLOATING_POINT_EMULATED = 1,

	[Description("The atomic compare and exchange operation (cmpxchg) is available.")]
	PF_COMPARE_EXCHANGE_DOUBLE = 2,

	[Description("The MMX instruction set is available.")]
	PF_MMX_INSTRUCTIONS_AVAILABLE = 3,

	[Description("The SSE instruction set is available.")]
	PF_XMMI_INSTRUCTIONS_AVAILABLE = 6,

	[Description("The 3D-Now instruction set is available.")]
	PF_3DNOW_INSTRUCTIONS_AVAILABLE = 7,

	[Description("The RDTSC instruction is available.")]
	PF_RDTSC_INSTRUCTION_AVAILABLE = 8,

	[Description("The processor is PAE-enabled. For more information, see Physical Address Extension.")]
	PF_PAE_ENABLED = 9,

	[Description("The SSE2 instruction set is available.")]
	PF_XMMI64_INSTRUCTIONS_AVAILABLE = 10,

	[Description("Data execution prevention is enabled.")]
	PF_NX_ENABLED = 12,

	[Description("The SSE3 instruction set is available.")]
	PF_SSE3_INSTRUCTIONS_AVAILABLE = 13,

	[Description("The atomic compare and exchange 128-bit operation (cmpxchg16b) is available.")]
	PF_COMPARE_EXCHANGE128 = 14,

	[Description("The atomic compare 64 and exchange 128-bit operation (cmp8xchg16) is available.")]
	PF_COMPARE64_EXCHANGE128 = 15,

	[Description("The processor channels are enabled.")]
	PF_CHANNELS_ENABLED = 16,

	[Description("The processor implements the XSAVE and XRSTOR instructions.")]
	PF_XSAVE_ENABLED = 17,

	[Description("The VFP/Neon: 32 x 64bit register bank is present. This flag has the same meaning as PF_ARM_VFP_EXTENDED_REGISTERS.")]
	PF_ARM_VFP_32_REGISTERS_AVAILABLE = 18,

	[Description("Second Level Address Translation is supported by the hardware.")]
	PF_SECOND_LEVEL_ADDRESS_TRANSLATION = 20,

	[Description("Virtualization is enabled in the firmware and made available by the operating system.")]
	PF_VIRT_FIRMWARE_ENABLED = 21,

	[Description("RDFSBASE, RDGSBASE, WRFSBASE, and WRGSBASE instructions are available.")]
	PF_RDWRFSGSBASE_AVAILABLE = 22,

	[Description("_fastfail() is available.")]
	PF_FASTFAIL_AVAILABLE = 23,

	[Description("The divide instructions are available.")]
	PF_ARM_DIVIDE_INSTRUCTION_AVAILABLE = 24,

	[Description("The 64-bit load/store atomic instructions are available.")]
	PF_ARM_64BIT_LOADSTORE_ATOMIC = 25,

	[Description("The external cache is available.")]
	PF_ARM_EXTERNAL_CACHE_AVAILABLE = 26,

	[Description("The floating-point multiply-accumulate instruction is available.")]
	PF_ARM_FMAC_INSTRUCTIONS_AVAILABLE = 27,

	[Description("This Arm processor implements the Arm v8 instructions set.")]
	PF_ARM_V8_INSTRUCTIONS_AVAILABLE = 29,

	[Description("This Arm processor implements the Arm v8 extra cryptographic instructions (for example, AES, SHA1 and SHA2).")]
	PF_ARM_V8_CRYPTO_INSTRUCTIONS_AVAILABLE = 30,

	[Description("This Arm processor implements the Arm v8 extra CRC32 instructions.")]
	PF_ARM_V8_CRC32_INSTRUCTIONS_AVAILABLE = 31,

	[Description("This Arm processor implements the Arm v8.1 atomic instructions (for example, CAS, SWP).")]
	PF_ARM_V81_ATOMIC_INSTRUCTIONS_AVAILABLE = 34,

	[Description("The SSSE3 instruction set is available.")]
	PF_SSSE3_INSTRUCTIONS_AVAILABLE = 36,

	[Description("The SSE4_1 instruction set is available.")]
	PF_SSE4_1_INSTRUCTIONS_AVAILABLE = 37,

	[Description("The SSE4_2 instruction set is available.")]
	PF_SSE4_2_INSTRUCTIONS_AVAILABLE = 38,

	[Description("The AVX instruction set is available.")]
	PF_AVX_INSTRUCTIONS_AVAILABLE = 39,

	[Description("The AVX2 instruction set is available.")]
	PF_AVX2_INSTRUCTIONS_AVAILABLE = 40,

	[Description("The AVX512F instruction set is available.")]
	PF_AVX512F_INSTRUCTIONS_AVAILABLE = 41,

	[Description("This Arm processor implements the Arm v8.2 DP instructions (for example, SDOT, UDOT). This feature is optional in Arm v8.2 implementations and mandatory in Arm v8.4 implementations.")]
	PF_ARM_V82_DP_INSTRUCTIONS_AVAILABLE = 43,

	[Description("This Arm processor implements the Arm v8.3 JSCVT instructions (for example, FJCVTZS).")]
	PF_ARM_V83_JSCVT_INSTRUCTIONS_AVAILABLE = 44,

	[Description("This Arm processor implements the Arm v8.3 LRCPC instructions (for example, LDAPR). Note that certain Arm v8.2 CPUs may optionally support the LRCPC instructions.")]
	PF_ARM_V83_LRCPC_INSTRUCTIONS_AVAILABLE = 45
}

public class CpuFeature
{
	public ProcessorFeature FeatureCode {get; set;}
	public string FeatureDescription {get; set;}
	public bool HasFeature {get; set;}
}

public static class MyExtensions
{
	public static string GetDescription<T>(this T e) where T : IConvertible
	{
		if (e is Enum)
		{
			Type type = e.GetType();
			Array values = System.Enum.GetValues(type);

			foreach (uint val in values)
			{
				if (val == e.ToInt32(CultureInfo.InvariantCulture))
				{
					var memInfo = type.GetMember(type.GetEnumName(val));
					var descriptionAttribute = memInfo[0]
						.GetCustomAttributes(typeof(DescriptionAttribute), false)
						.FirstOrDefault() as DescriptionAttribute;

					if (descriptionAttribute != null)
					{
						return descriptionAttribute.Description;
					}
				}
			}
		}

		return null; 
	}
}

