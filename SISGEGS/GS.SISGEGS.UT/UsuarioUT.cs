using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GS.SISGEGS.BL;

namespace GS.SISGEGS.UT
{
    [TestClass]
    public class UsuarioUT
    {
        [TestMethod]
        public void Usuario_Autenticar()
        {
            UsuarioBL objUsuarioBL;
            int idUsuario;
            try {
                objUsuarioBL = new UsuarioBL();
                idUsuario = objUsuarioBL.Usuario_Autenticar(3, "RCHAVEZA", "1").idUsuario;
                idUsuario = objUsuarioBL.Usuario_Login(2).idUsuario;
                Assert.Equals(idUsuario, 2);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
