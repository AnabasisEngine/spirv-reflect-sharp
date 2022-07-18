using System.Runtime.InteropServices;
using System.Text;

namespace SpirvReflectSharp;

public class ShaderModule : IDisposable
{
    /// <summary>
    /// The compiler that generated this SPIR-V module
    /// </summary>
    public ReflectGenerator Generator;

    public string              EntryPointName;
    public uint                EntryPointId;
    public uint                EntryPointCount;
    public ReflectEntryPoint[] EntryPoints;

    public SourceLanguage SourceLanguage;
    public uint           SourceLanguageVersion;

    public string SourceFile;
    public string SourceSource;

    public ExecutionModel     SpirvExecutionModel;
    public ReflectShaderStage ShaderStage;

    public unsafe ReflectInterfaceVariable[] EnumerateInputVariables() {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            uint varCount = 0;
            SpirvReflectNative.SpvReflectResult result = SpirvReflectNative.spvReflectEnumerateInputVariables(inmodule, &varCount, null);

            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

            SpirvReflectNative.SpvReflectInterfaceVariable** inputVars =
                stackalloc SpirvReflectNative.SpvReflectInterfaceVariable*[(int)(varCount *
                    sizeof(SpirvReflectNative.SpvReflectInterfaceVariable))];

            result = SpirvReflectNative.spvReflectEnumerateInputVariables(inmodule, &varCount, inputVars);
            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

            // Convert to managed
            return ReflectInterfaceVariable.ToManaged(inputVars, varCount);
        }
    }

    public unsafe ReflectInterfaceVariable[] EnumerateEntryPointInputVariables(string name) {
        IntPtr ptr = Marshal.StringToHGlobalAnsi(name);
        try {
            fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
                uint varCount = 0;
                SpirvReflectNative.SpvReflectResult result =
                    SpirvReflectNative.spvReflectEnumerateEntryPointInputVariables(inmodule, (sbyte*)ptr, &varCount,
                        null);

                SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

                SpirvReflectNative.SpvReflectInterfaceVariable** inputVars =
                    stackalloc SpirvReflectNative.SpvReflectInterfaceVariable*[(int)(varCount *
                        sizeof(SpirvReflectNative.SpvReflectInterfaceVariable))];

                result = SpirvReflectNative.spvReflectEnumerateEntryPointInputVariables(inmodule, (sbyte*)ptr,
                    &varCount, inputVars);
                SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

                // Convert to managed
                return ReflectInterfaceVariable.ToManaged(inputVars, varCount);
            }
        }
        finally {
            Marshal.FreeHGlobal(ptr);
        }
    }

    public unsafe ReflectInterfaceVariable[] EnumerateOutputVariables() {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            uint varCount = 0;
            SpirvReflectNative.SpvReflectResult result = SpirvReflectNative.spvReflectEnumerateOutputVariables(inmodule, &varCount, null);

            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

            SpirvReflectNative.SpvReflectInterfaceVariable** outputVars =
                stackalloc SpirvReflectNative.SpvReflectInterfaceVariable*[(int)(varCount *
                    sizeof(SpirvReflectNative.SpvReflectInterfaceVariable))];

            result = SpirvReflectNative.spvReflectEnumerateOutputVariables(inmodule, &varCount, outputVars);
            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

            // Convert to managed
            return ReflectInterfaceVariable.ToManaged(outputVars, varCount);
        }
    }

    public unsafe ReflectInterfaceVariable[] EnumerateInterfaceVariables() {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            uint varCount = 0;
            SpirvReflectNative.SpvReflectResult result = SpirvReflectNative.spvReflectEnumerateInterfaceVariables(inmodule, &varCount, null);

            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

            SpirvReflectNative.SpvReflectInterfaceVariable** interfaceVars =
                stackalloc SpirvReflectNative.SpvReflectInterfaceVariable*[(int)(varCount *
                    sizeof(SpirvReflectNative.SpvReflectInterfaceVariable))];

            result = SpirvReflectNative.spvReflectEnumerateInterfaceVariables(inmodule, &varCount, interfaceVars);
            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

            // Convert to managed
            return ReflectInterfaceVariable.ToManaged(interfaceVars, varCount);
        }
    }

    public unsafe ReflectBlockVariable[] EnumeratePushConstants() {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            uint varCount = 0;
            SpirvReflectNative.SpvReflectResult result = SpirvReflectNative.spvReflectEnumeratePushConstants(inmodule, &varCount, null);

            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

            SpirvReflectNative.SpvReflectBlockVariable** pushConsts =
                stackalloc SpirvReflectNative.SpvReflectBlockVariable*[(int)(varCount *
                                                                             sizeof(SpirvReflectNative.
                                                                                 SpvReflectBlockVariable))];

            result = SpirvReflectNative.spvReflectEnumeratePushConstants(inmodule, &varCount, pushConsts);
            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

            // Convert to managed
            return ReflectBlockVariable.ToManaged(pushConsts, varCount);
        }
    }

    public unsafe uint GetCodeSize() {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            return SpirvReflectNative.spvReflectGetCodeSize(inmodule);
        }
    }

    public unsafe string GetCode() {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            return new string(SpirvReflectNative.spvReflectGetCode(inmodule));
        }
    }

    public unsafe ReflectInterfaceVariable GetInputVariable(uint location) {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            ReflectInterfaceVariable reflt = new ReflectInterfaceVariable();
            SpirvReflectNative.SpvReflectResult result =
                SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_NOT_READY;
            SpirvReflectNative.SpvReflectInterfaceVariable* nativeOut = SpirvReflectNative.spvReflectGetInputVariable(inmodule, location, &result);
            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);
            ReflectInterfaceVariable.PopulateReflectInterfaceVariable(ref *nativeOut, ref reflt);
            return reflt;
        }
    }

    public unsafe ReflectInterfaceVariable GetInputVariableByLocation(uint location) {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            ReflectInterfaceVariable reflt = new ReflectInterfaceVariable();
            SpirvReflectNative.SpvReflectResult result =
                SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_NOT_READY;
            SpirvReflectNative.SpvReflectInterfaceVariable* nativeOut = SpirvReflectNative.spvReflectGetInputVariableByLocation(inmodule, location, &result);
            SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);
            ReflectInterfaceVariable.PopulateReflectInterfaceVariable(ref *nativeOut, ref reflt);
            return reflt;
        }
    }

    public unsafe ReflectInterfaceVariable GetInputVariableBySemantic(string semantic) {
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            ReflectInterfaceVariable reflt = new ReflectInterfaceVariable();
            SpirvReflectNative.SpvReflectResult result =
                SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_NOT_READY;
            byte[] semanticBytes = Encoding.ASCII.GetBytes(semantic);

            fixed (byte* ptr = semanticBytes) {
                SpirvReflectNative.SpvReflectInterfaceVariable* nativeOut = SpirvReflectNative.spvReflectGetInputVariableBySemantic(inmodule, (sbyte*)ptr, &result);
                SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);
                ReflectInterfaceVariable.PopulateReflectInterfaceVariable(ref *nativeOut, ref reflt);
                return reflt;
            }
        }
    }

    #region Unmanaged

    internal unsafe ShaderModule(SpirvReflectNative.SpvReflectShaderModule module) {
        NativeShaderModule = module;

        // Convert to managed
        Generator = (ReflectGenerator)module.generator;
        EntryPointName = new string(module.entry_point_name);
        EntryPointId = module.entry_point_id;
        SourceLanguage = (SourceLanguage)module.source_language;
        SourceLanguageVersion = module.source_language_version;
        SpirvExecutionModel = (ExecutionModel)module.spirv_execution_model;
        ShaderStage = (ReflectShaderStage)module.shader_stage;
        SourceFile = new string(module.source_file);
        SourceSource = new string(module.source_source);

        // Entry point extraction
        EntryPoints = new ReflectEntryPoint[module.entry_point_count];
        for (int i = 0; i < module.entry_point_count; i++) {
            EntryPoints[i] = new ReflectEntryPoint() {
                Id = module.entry_points[i].id,
                Name = new string(module.entry_points[i].name),
                ShaderStage = (ReflectShaderStage)module.entry_points[i].shader_stage,
                SpirvExecutionModel = (ExecutionModel)module.entry_points[i].spirv_execution_model,

                UsedPushConstants = new uint[module.entry_points[i].used_push_constant_count],
                UsedUniforms = new uint[module.entry_points[i].used_uniform_count],

                DescriptorSets = new ReflectDescriptorSet[module.entry_points[i].descriptor_set_count]
            };
            // Enumerate used push constants
            for (int j = 0; j < module.entry_points[i].used_push_constant_count; j++) {
                EntryPoints[i].UsedPushConstants[j] = module.entry_points[i].used_push_constants[j];
            }

            // Enumerate used uniforms
            for (int j = 0; j < module.entry_points[i].used_uniform_count; j++) {
                EntryPoints[i].UsedUniforms[j] = module.entry_points[i].used_uniforms[j];
            }

            // Enumerate descriptor sets
            for (int j = 0; j < module.entry_points[i].descriptor_set_count; j++) {
                SpirvReflectNative.SpvReflectDescriptorSet desc = module.entry_points[i].descriptor_sets[j];
                EntryPoints[i].DescriptorSets[j].Set = desc.set;
                EntryPoints[i].DescriptorSets[j].Bindings = new ReflectDescriptorBinding[desc.binding_count];

                for (int k = 0; k < desc.binding_count; k++) {
                    EntryPoints[i].DescriptorSets[j].Bindings[k] = new ReflectDescriptorBinding(*desc.bindings[k]);
                }
            }
        }
    }

    /// <summary>
    /// The native shader module
    /// </summary>
    public SpirvReflectNative.SpvReflectShaderModule NativeShaderModule;

    public bool Disposed;

    public unsafe void Dispose(bool freeManaged) {
        if (Disposed) return;
        fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule) {
            SpirvReflectNative.spvReflectDestroyShaderModule(inmodule);
        }

        Disposed = true;
    }

    public void Dispose() {
        Dispose(true);
    }

    ~ShaderModule() {
        Dispose(false);
    }

    #endregion
}