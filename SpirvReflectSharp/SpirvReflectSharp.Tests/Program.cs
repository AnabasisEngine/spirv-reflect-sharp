﻿using System.IO;

namespace SpirvReflectSharp.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] shaderBytes = File.ReadAllBytes(@"E:\Data\Projects\Vessel\VesselSharp\spirv-reflect-sharp\pbr_khr.frag.spv");
			using (ShaderModule module = SpirvReflect.ReflectCreateShaderModule(shaderBytes))
			{
				var in_vars = module.EnumerateInputVariables();
				var intf_vars = module.EnumerateInterfaceVariables();
				var out_vars = module.EnumerateOutputVariables();
				var push_constants = module.EnumeratePushConstants();

				var getVar = module.GetInputVariable(2);
			}

		}
	}
}
