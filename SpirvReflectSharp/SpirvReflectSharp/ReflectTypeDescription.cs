namespace SpirvReflectSharp;

public struct ReflectTypeDescription
{
	public uint                     Id;
	public Op                       Op;
	public string                   TypeName;
	public string                   StructMemberName;
	public StorageClass             StorageClass;
	public ReflectType              TypeFlags;
	public ReflectDecoration        DecorationFlags;
	public Traits                   Traits;
	public ReflectTypeDescription[] Members;

	public override string ToString()
	{
		return "ReflectTypeDescription {" + StructMemberName + " " + TypeFlags + "} [" + Members.Length + "]";
	}

	internal static unsafe ReflectTypeDescription GetManaged(ref SpirvReflectNative.SpvReflectTypeDescription typeDescription)
	{
		ReflectTypeDescription desc = new();

		PopulateReflectTypeDescription(ref typeDescription, ref desc);
		desc.Members = ToManagedArray(typeDescription.members, typeDescription.member_count);

		return desc;
	}

	private static unsafe ReflectTypeDescription[] ToManagedArray(SpirvReflectNative.SpvReflectTypeDescription* typeDescription, uint memberCount)
	{
		ReflectTypeDescription[] intfVars = new ReflectTypeDescription[memberCount];

		for (int i = 0; i < memberCount; i++)
		{
			SpirvReflectNative.SpvReflectTypeDescription typedesc = typeDescription[i];
			ReflectTypeDescription variable = new();

			PopulateReflectTypeDescription(ref typedesc, ref variable);
			variable.Members = ToManagedArray(typedesc.members, typedesc.member_count);

			intfVars[i] = variable;
		}

		return intfVars;
	}

	private static unsafe void PopulateReflectTypeDescription(
		ref SpirvReflectNative.SpvReflectTypeDescription typeDescription,
		ref ReflectTypeDescription desc)
	{
		desc.Id = typeDescription.id;
		desc.Op = (Op)typeDescription.op;
		desc.TypeName = new string(typeDescription.type_name);
		desc.StructMemberName = new string(typeDescription.struct_member_name);
		desc.StorageClass = (StorageClass)typeDescription.storage_class;
		desc.TypeFlags = (ReflectType)typeDescription.type_flags.Data;
		desc.DecorationFlags = (ReflectDecoration)typeDescription.decoration_flags.Data;
		desc.Traits = new Traits(typeDescription.traits);
	}
}