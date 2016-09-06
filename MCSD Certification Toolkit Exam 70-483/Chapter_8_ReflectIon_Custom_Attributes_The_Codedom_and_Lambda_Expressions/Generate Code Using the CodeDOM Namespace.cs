namespace ConsoleApplication16.Chapter_8_ReflectIon_Custom_Attributes_The_Codedom_and_Lambda_Expressions
{
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Reflection;

    class Generate_Code_Using_the_CodeDOM_Namespace
    {
        public static void Example()
        {
            // CodeCompileUnit
            var codeCompileUnit = new CodeCompileUnit();
            // CodeNamespace and CodeNamespaceImport
            var codeNamespace = new CodeNamespace("Reflection");
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Text"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Threading.Tasks"));
            codeCompileUnit.Namespaces.Add(codeNamespace);
            // CodeTypeDeclaration
            var targetClass = new CodeTypeDeclaration("Calculator")
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public
            };
            // Add the class to the namespace
            codeNamespace.Types.Add(targetClass);
            // CodeMemberField
            var xField = new CodeMemberField
            {
                Name = "x",
                Type = new CodeTypeReference(typeof(double))
            };
            targetClass.Members.Add(xField);
            var yField = new CodeMemberField
            {
                Name = "y",
                Type = new CodeTypeReference(typeof(double))
            };
            targetClass.Members.Add(yField);
            // CodeMemberProperty
            // X Property
            var xProperty = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = "X",
                HasGet = true,
                HasSet = true,
                Type = new CodeTypeReference(typeof(double))
            };
            xProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(),
                    "x")));
            xProperty.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        "x"),
                    new CodePropertySetValueReferenceExpression()));
            targetClass.Members.Add(xProperty);
            // Y Property
            var yProperty = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = "Y",
                HasGet = true,
                HasSet = true,
                Type = new CodeTypeReference(typeof(double))
            };
            yProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        "y")));
            yProperty.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        "y"),
                    new CodePropertySetValueReferenceExpression()));
            targetClass.Members.Add(yProperty);
            // CodeMemberMethod
            var divideMethod = new CodeMemberMethod
            {
                Name = "Divide",
                ReturnType = new CodeTypeReference(typeof(double)),
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };
            var ifLogic = new CodeConditionStatement
            {
                Condition = new CodeBinaryOperatorExpression(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        yProperty.Name),
                    CodeBinaryOperatorType.ValueEquality,
                    new CodePrimitiveExpression(0))
            };
            ifLogic.TrueStatements.Add(
                new CodeMethodReturnStatement(
                    new CodePrimitiveExpression(0)));
            ifLogic.FalseStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeBinaryOperatorExpression(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(),
                            xProperty.Name),
                        CodeBinaryOperatorType.Divide,
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(),
                            yProperty.Name))));
            divideMethod.Statements.Add(ifLogic);
            targetClass.Members.Add(divideMethod);
            // CodeParameterDeclarationExpression and CodeMethodInvokeExpression
            var exponentMethod = new CodeMemberMethod
            {
                Name = "Exponent",
                ReturnType = new CodeTypeReference(typeof(double)),
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };
            var powerParameter = new CodeParameterDeclarationExpression(typeof(double), "power");
            exponentMethod.Parameters.Add(powerParameter);
            var callToMath = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("System.Math"),
                "Pow",
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    xProperty.Name),
                new CodeArgumentReferenceExpression("power"));
            exponentMethod.Statements.Add(new CodeMethodReturnStatement(callToMath));
            targetClass.Members.Add(exponentMethod);
            // CodeDOMProvider
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions
            {
                BlankLinesBetweenMembers = false,
                BracingStyle = "C"
            };
            using (StreamWriter sourceWriter = new StreamWriter(string.Format("Calculator.{0}", provider.FileExtension)))
            {
                provider.GenerateCodeFromCompileUnit(codeCompileUnit, sourceWriter, options);
            }
        }
    }
}