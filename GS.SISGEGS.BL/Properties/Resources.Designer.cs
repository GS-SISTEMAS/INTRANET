﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS.SISGEGS.BL.Properties {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GS.SISGEGS.BL.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a 
        ///select 
        ///periodoYearE,
        ///periodoMesE,
        ///sum(venta) as TotalVentaMes,
        ///sum(cobradoDoc) as totalMes
        ///from 
        ///(
        ///select
        ///t7.opOrigen,
        ///t7.anhoOrigen as periodoYearE,
        ///t7.mesOrigen as periodoMesE,
        ///t7.VentaOrigen as venta,
        ///sum(t7.PagadoDolares) as Cobrado,
        ///sum(t7.cobrado) as cobradoDoc
        ///from
        ///(
        ///select 
        ///t6.op as opOrigen,
        ///t6.TablaOrigen as TablaOrigen,
        ///t6.anho as anhoOrigen,
        ///t6.mes as mesOrigen,
        ///t6.Venta as VentaOrigen,
        ///t6.TablaDestino as TablaDestinoOrigen,
        ///t6.OpDestino as opDestinoOrigen,
        ///t6.porcenta [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string SelectCobrandoMes {
            get {
                return ResourceManager.GetString("SelectCobrandoMes", resourceCulture);
            }
        }
    }
}
