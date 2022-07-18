namespace SpirvReflectSharp;

public struct ReflectInterfaceVariable
{
	public uint                       SpirvId;
	public string                     Name;
	public uint                       Location;
	public StorageClass               StorageClass;
	public string                     Semantic;
	public ReflectDecoration          DecorationFlags;
	public BuiltIn                    BuiltIn;
	public ReflectNumericTraits       Numeric;
	public ReflectArrayTraits         Array;
	public ReflectInterfaceVariable[] Members;
	public ReflectFormat              Format;
	public ReflectTypeDescription     TypeDescription;

	public override string ToString() => "ReflectInterfaceVariable {" + Name + "} [" + Members.Length + "]";

	internal static unsafe ReflectInterfaceVariable[] ToManaged(SpirvReflectNative.SpvReflectInterfaceVariable** inputVars, uint varCount)
	{
		ReflectInterfaceVariable[] intfVars = new ReflectInterfaceVariable[varCount];

		for (int i = 0; i < varCount; i++)
		{
			SpirvReflectNative.SpvReflectInterfaceVariable* interfaceVarNative = inputVars[i];
			SpirvReflectNative.SpvReflectInterfaceVariable intf = *interfaceVarNative;
			ReflectInterfaceVariable variable = new();

			PopulateReflectInterfaceVariable(ref intf, ref variable);
			variable.Members = ToManagedArray(intf.members, intf.member_count);

			intfVars[i] = variable;
		}

		return intfVars;
	}

	private static unsafe ReflectInterfaceVariable[] ToManagedArray(SpirvReflectNative.SpvReflectInterfaceVariable* inputVars, uint varCount)
	{
		ReflectInterfaceVariable[] intfVars = new ReflectInterfaceVariable[varCount];

		for (int i = 0; i < varCount; i++)
		{
			SpirvReflectNative.SpvReflectInterfaceVariable intf = inputVars[i];
			ReflectInterfaceVariable variable = new();

			PopulateReflectInterfaceVariable(ref intf, ref variable);
			variable.Members = ToManagedArray(intf.members, intf.member_count);

			intfVars[i] = variable;
		}

		return intfVars;
	}

	internal static unsafe void PopulateReflectInterfaceVariable (
		ref SpirvReflectNative.SpvReflectInterfaceVariable intf,
		ref ReflectInterfaceVariable variable)
	{
		variable.SpirvId = intf.spirv_id;
		variable.Name = new string(intf.name);
		variable.Location = intf.location;
		variable.StorageClass = (StorageClass)intf.storage_class;
		variable.Semantic = new string(intf.semantic);
		variable.DecorationFlags = (ReflectDecoration)intf.decoration_flags.Data;
		variable.BuiltIn = (BuiltIn)intf.built_in;
		variable.Format = (ReflectFormat)intf.format;
		variable.TypeDescription = ReflectTypeDescription.GetManaged(ref *intf.type_description);
		variable.Array = new ReflectArrayTraits(intf.array);
		variable.Numeric = new ReflectNumericTraits(intf.numeric);


	}
}