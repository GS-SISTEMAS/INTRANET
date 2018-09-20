using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
using System.Data;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ILetrasEmitidasWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ILetrasEmitidasWCF
    {
        [OperationContract]
        List<gsLetrasEmitidas_ListarResult> LetrasEmitidas_Listar(int idEmpresa, int codigoUsuario, string codAgenda, string opFinan, DateTime fechaInicial, DateTime fechaFinal);
        [OperationContract]
        List<gsLetrasEmitidas_CabeceraResult> LetrasEmitidas_Cabecera(int idEmpresa, int codigoUsuario, string opFinan);
        [OperationContract]
        List<gsLetrasEmitidas_DocumentosResult> LetrasEmitidas_Documentos(int idEmpresa, int codigoUsuario, string opFinan);
        [OperationContract]
        List<gsLetrasEmitidas_LetrasResult> LetrasEmitidas_Letras(int idEmpresa, int codigoUsuario, string opFinan);
        [OperationContract]
        List<gsProcesoLetras_ListarResult> ProcesoLetras_NumerosUnicos(int idEmpresa, int codigoUsuario, int anho, int mes, string descripcion);

        [OperationContract]
        void NumerosUnicos_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DataTable tabla);
        [OperationContract]
        int ProcesoLetras_NumerosUnicos_Insertar(int idEmpresa, int codigoUsuario, int anho, int mes, int dia, string descripcion, int ID);
        [OperationContract]
        void NumerosUnicos_Registrar_Proceso(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsNumerosUnicos_ListarExportarResult> ProcesoLetras_NumerosUnicos_Listar(int idEmpresa, int codigoUsuario, int id_proceso);

        [OperationContract]
        List<VBG01425Result> LetrasElectronicas_Individual(int idEmpresa, int codigoUsuario, int id_Letra);
        [OperationContract]
        List<gsLetrasElectronicas_ListarResult> LetrasElectronicas_Listar(int idEmpresa, int codigoUsuario, int id_OP);

        [OperationContract]
        void Registrar_LogLetrasDescargadas(int idEmpresa, int codigoUsuario, Int32 idletra, string usuariointranet, Int32 Op_DocVenta);

        [OperationContract]
        List<USP_SEL_Canje_Automatico_LetrasResult> CanjeAutomaticoLetras_Listar(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, string Estado);

        [OperationContract]
        string CanjeAutomaticoLetras_Registrar(string Empresa, int ID_Letra, string Usuario);

        [OperationContract]
        int Financiamiento_CA_Letras_General(string Empresa);

        [OperationContract]
        List<USP_SEL_Estado_LetrasResult> Estado_Letras_Listar(string idEmpresa);

        [OperationContract]
        List<USP_SEL_Porcentaje_Avance_LetrasResult> Porcentaje_Avance_Letras_Lista(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<USP_SEL_Porcentaje_Avance_Letras_ZonasResult> Porcentaje_Avance_Letras_Lista_x_Zonas(int idEmpresa, int codigoUsuario);


    }
}
