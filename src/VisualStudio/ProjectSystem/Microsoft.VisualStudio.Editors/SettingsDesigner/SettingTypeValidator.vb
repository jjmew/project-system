'------------------------------------------------------------------------------
' <copyright from='2003' to='2005' company='Microsoft Corporation'>           
'    Copyright (c) Microsoft Corporation. All Rights Reserved.                
'    Information Contained Herein is Proprietary and Confidential.            
' </copyright>                                                                
'------------------------------------------------------------------------------
'

Imports System
Imports system.ComponentModel

Imports Microsoft.VisualStudio.Shell.Design.Serialization

Namespace Microsoft.VisualStudio.Editors.SettingsDesigner
    ''' <summary>
    ''' Manages the decisions on if a type is valid as a setting type and/or
    ''' is obsolete
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class SettingTypeValidator

        ''' <summary>
        ''' Indicate if the type has changed from the version that is currently loaded in this app domain
        ''' </summary>
        ''' <param name="type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsTypeObsolete(ByVal type As System.Type) As Boolean
            Dim typeInLoadedAssembly As System.Type = System.Type.GetType(type.AssemblyQualifiedName)

            ' If the type we get from System.Type.GetType(<assembly qualified type name>) is not the same
            ' as the type provided, something has changed in the defining assembly
            Return Object.ReferenceEquals(type, typeInLoadedAssembly) = False
        End Function

        Public Shared Function IsValidSettingType(ByVal type As System.Type) As Boolean

            If Not type.IsPublic Then Return False
            If type.IsPointer Then Return False
            If type.IsGenericType Then Return False
            If type Is GetType(System.Void) Then Return False
            If Not (type.IsClass OrElse type.IsValueType) Then Return False
            If Not CanSerializeType(type) Then Return False

            Return True
        End Function

        Private Shared Function CanSerializeType(ByVal type As System.Type) As Boolean
            Try
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(type)
                If tc.CanConvertFrom(GetType(String)) AndAlso tc.CanConvertTo(GetType(String)) Then
                    Return True
                End If
                If type.GetConstructor(Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public, Nothing, System.Reflection.CallingConventions.HasThis, System.Type.EmptyTypes, Nothing) IsNot Nothing Then
                    Return True
                End If
            Catch ex As Exception
                ' Since we have no idea what the custom type descriptors may do, we catch everything that we can and 
                ' pretend that nothing too bad happened...
            End Try
            Return False
        End Function

    End Class
End Namespace
