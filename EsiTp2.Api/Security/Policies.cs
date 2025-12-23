namespace EsiTp2.Api.Security
{
    public static class Policies
    {
      
        
            // Morador, Gestor e Admin podem ver sensores
            public const string VerSensores = "VerSensores";

            // Só Gestor e Admin podem criar/editar sensores
            public const string GerirSensores = "GerirSensores";

            
            // public const string ApagarSensores = "ApagarSensores";
        
    }

}
