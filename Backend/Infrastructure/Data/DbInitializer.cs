using System.Globalization;
using BackendApi.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Appliquer les migrations si besoin
        await context.Database.MigrateAsync();
     
        if (!context.Teams.Any())
        {
            var teams = new List<TeamDao>
            {
                new TeamDao { Label = "directeur" , Code = "1"},
                new TeamDao { Label = "CTO", Code = "2" },
                new TeamDao { Label = "Responsable_Département", Code = "3" },
                new TeamDao { Label = "Responsable_Technique" , Code = "4"},
                new TeamDao { Label = "CROCO", Code = "5" },
                new TeamDao { Label = "GRIFFON" , Code = "6"},
                new TeamDao { Label = "TOUCAN", Code = "7" },
                new TeamDao { Label = "Architecture" , Code = "8"},
                new TeamDao { Label = "TIGER" , Code = "9"},
                new TeamDao { Label = "PHÉNIX" , Code = "10"},
                new TeamDao { Label = "GIRAFE", Code = "11" },
                new TeamDao { Label = "CASTOR", Code = "12" },
                new TeamDao { Label = "JAGUAR" , Code = "13"},
                new TeamDao { Label = "Responsable Technique", Code = "14" },
                new TeamDao { Label = "HIBOU" , Code = "15"},
                new TeamDao { Label = "COBRA", Code = "16" },
                new TeamDao { Label = "LYNX" , Code = "17"},
                new TeamDao { Label = "PANTHER" , Code = "18"},
                new TeamDao { Label = "Support_N2" , Code = "19"},
                new TeamDao { Label = "QUOKKA", Code = "20" },
                new TeamDao { Label = "CAMELEON" , Code = "21"},
                new TeamDao { Label = "UX-UI" , Code = "22"}
            };

            context.Teams.AddRange(teams);
            await context.SaveChangesAsync(); 
        }
        
        if (!context.Products.Any())
        {
            var products = new List<ProductDao>
            {
                new ProductDao { Label = "AI_HUB", TeamId  = 21, Code = "1"},
                new ProductDao { Label = "AI_ENGINE",TeamId  = 21, Code = "2" },
                new ProductDao { Label = "AUDIO_SCAN",TeamId  = 21, Code = "3"},
                
                new ProductDao { Label = "ELLO_AUTO_SPECIALITÉS",TeamId  = 12, Code = "4" },
                new ProductDao { Label = "SI_CREATIV",TeamId  = 12, Code = "5" },
                
                new ProductDao { Label = "ELLO_WIZIOU",TeamId  = 16, Code = "6"},
                new ProductDao { Label = "ESPACE_BTOC_IRD",TeamId  = 16, Code = "7"},
                new ProductDao { Label = "OUVERTURE_BOT",TeamId  = 16, Code = "8"},
                new ProductDao { Label = "SINAPPS_IRD",TeamId  = 16, Code = "9"},
                
                new ProductDao { Label = "ESPACE_PARTENAIRE_IRD",TeamId  = 5,Code = "10"},
                new ProductDao { Label = "EXTRANET_ASSURÉ_IRD",TeamId  = 5,Code = "11"},
                new ProductDao { Label = "iTOP",TeamId  = 5,  Code = "12"},
                new ProductDao { Label = "myROADIA", TeamId  = 5, Code = "13"},
                new ProductDao { Label = "SI_KPI",TeamId  = 5,  Code = "14"},
                
                new ProductDao { Label = "APPLICATION_SUPPORT_N2", TeamId  = 11, Code = "15"},
                new ProductDao { Label = "BPM_ADENES", TeamId  = 11, Code = "16"},
                new ProductDao { Label = "PUSH_BOT", TeamId  = 11,Code = "17"},
                
                new ProductDao { Label = "CONNECTEUR_CLIENT", TeamId  = 6,Code = "18"},
                new ProductDao { Label = "ELENA", TeamId  = 6, Code = "19"},
                new ProductDao { Label = "ELLO_CONFIG",TeamId  = 6, Code = "20"},
                new ProductDao { Label = "MILOS",TeamId  = 6, Code = "21"},
                
                new ProductDao { Label = "DOC_SCAN",TeamId  = 15, Code = "22"},
                new ProductDao { Label = "ELLO_SATISFACTION",TeamId  = 15,  Code = "23"},
                new ProductDao { Label = "ELLO_SIGN",TeamId  = 15, Code = "24"},
                new ProductDao { Label = "ROBOT_GED_3C", TeamId  = 15, Code = "25"},
                new ProductDao { Label = "ROBOT_GED_ELEX", TeamId  = 15, Code = "26"},
                new ProductDao { Label = "TO_ELLO",TeamId  = 15, Code = "27"},
                
                new ProductDao { Label = "ELLO_AUTO", TeamId  = 13,Code = "28"},
                
                new ProductDao { Label = "ELLO_ADJUSTERS",TeamId  = 18,Code = "29"},
                new ProductDao { Label = "ELLO_HONO",TeamId  = 18, Code = "30"},
                new ProductDao { Label = "ELLO_INVOICE", TeamId  = 18,Code = "31"},
                
                new ProductDao { Label = "ELLO_AUDIT", TeamId  = 10, Code = "32"},
                new ProductDao { Label = "ELLO_PLANNER", TeamId  = 10,Code = "33"},
                
                new ProductDao { Label = "APPLICATION_ADENES",TeamId  = 9,  Code = "34"},
                new ProductDao { Label = "ELLO_WORLD",TeamId  = 9,  Code = "35"},
                new ProductDao { Label = "ELLO_WORLD_CONFIG",TeamId  = 9, Code = "36"},
                new ProductDao { Label = "HERMES_CORP",TeamId  = 9,Code = "37"},
                
                new ProductDao { Label = "ELLO_EXPERT", TeamId  = 7, Code = "38"},
                new ProductDao { Label = "ELLO_GED",TeamId  = 7, Code = "39"},
                new ProductDao { Label = "ELLO_YOU", TeamId  = 7, Code = "40"},
                new ProductDao { Label = "HERMES_FR",TeamId  = 7,  Code = "41"},
                new ProductDao { Label = "ULYSSE", TeamId  = 7,Code = "42"},
                
                new ProductDao { Label = "ALL", TeamId  = 19, Code = "43"},
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
        
        if (!context.Modules.Any())
        {
            var modules = new List<ModuleDao>
            {
                new ModuleDao { Label = "AI_HUB - Amélioration textuelle", ProductId = 1, SegmentCode = "GROUPE", Code = "37-3", StartDate = DateTime.ParseExact("10/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "AI_HUB - Scoring sécheresse", ProductId = 1, SegmentCode = "IRD", Code = "37-1", StartDate = DateTime.ParseExact("09/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "AI_HUB - socle", ProductId = 1, SegmentCode = "GROUPE", Code = "37-5", StartDate = DateTime.ParseExact("11/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "AI_HUB - Comparatif facture auto", ProductId = 1, SegmentCode = "AUTO", Code = "37-2", StartDate = DateTime.ParseExact("11/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "AI_HUB - Transmission audio", ProductId = 1, SegmentCode = "GROUPE", Code = "37-4", StartDate = DateTime.ParseExact("10/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "AI_ENGINE - socle", ProductId = 2, SegmentCode = "GROUPE", Code = "40-1", StartDate = DateTime.ParseExact("01/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
               
                new ModuleDao { Label = "AUDIO_SCAN - socle", ProductId = 3, SegmentCode = "GROUPE", Code = "16-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ELLO_AUTO_SPECIALITÉS - Module PJ", ProductId = 4, SegmentCode = "AUTO", Code = "1-1", StartDate = DateTime.ParseExact("01/02/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_AUTO_SPECIALITÉS - Module Etude de Marché", ProductId = 4, SegmentCode = "AUTO", Code = "1-2", StartDate = DateTime.ParseExact("01/02/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "ELLO_AUTO_SPECIALITÉS - Module Valeur théorique", ProductId = 4, SegmentCode = "AUTO", Code = "1-3", StartDate = DateTime.ParseExact("01/02/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "SI_CREATIV - Argonaute", ProductId = 5, SegmentCode = "AUTO", Code = "2-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "SI_CREATIV - Ea-sy PJ-RC", ProductId = 5, SegmentCode = "AUTO", Code = "2-2", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "SI_CREATIV - Intranet", ProductId = 5, SegmentCode = "AUTO", Code = "2-3", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "SI_CREATIV - Mon Auto & Compagnie", ProductId = 5, SegmentCode = "AUTO", Code = "2-4", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "SI_CREATIV - Outil Cas Complexe", ProductId = 5, SegmentCode = "AUTO", Code = "2-5", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "SI_CREATIV - Outil RH", ProductId = 5, SegmentCode = "AUTO", Code = "2-6", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "SI_CREATIV - Outil Support", ProductId = 5, SegmentCode = "AUTO", Code = "2-7", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "SI_CREATIV - Outil Users", ProductId = 5, SegmentCode = "AUTO", Code = "2-8", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "SI_CREATIV - Portail Stats", ProductId = 5, SegmentCode = "AUTO", Code = "2-9", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "SI_CREATIV - Sentinelle", ProductId = 5, SegmentCode = "AUTO", Code = "2-10", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "SI_CREATIV - Smartpix", ProductId = 5, SegmentCode = "AUTO", Code = "2-11", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "SI_CREATIV - Typhon", ProductId = 5, SegmentCode = "AUTO", Code = "2-12", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                
                new ModuleDao { Label = "ELLO_WIZIOU - socle", ProductId = 6, SegmentCode = "IRD France", Code = "13-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                
                new ModuleDao { Label = "ESPACE_BTOC_IRD - Extranet Qualitel", ProductId = 7, SegmentCode = "GROUPE", Code = "6-2", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ESPACE_BTOC_IRD - Extranet Client Izymo", ProductId = 7, SegmentCode = "GROUPE", Code = "6-3", StartDate = DateTime.ParseExact("01/03/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "OUVERTURE_BOT - socle", ProductId = 8, SegmentCode = "IRD France", Code = "12-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "E-AGORA", ProductId = 8, SegmentCode = "IRD France", Code = "12-2", StartDate = DateTime.ParseExact("03/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "SINAPPS_IRD - In", ProductId = 9, SegmentCode = "IRD France", Code = "14-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "SINAPPS_IRD - Out", ProductId = 9, SegmentCode = "IRD France", Code = "14-2", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "SINAPPS_IRD - API", ProductId = 9, SegmentCode = "IRD France", Code = "14-3", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ESPACE_PARTENAIRE_IRD - Extranet Assureur", ProductId = 10, SegmentCode = "GROUPE", Code = "6-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "ESPACE_PARTENAIRE_IRD - Extranet Vering", ProductId = 10, SegmentCode = "GROUPE", Code = "6-4", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ESPACE_PARTENAIRE_IRD - API Broker", ProductId = 10, SegmentCode = "GROUPE", Code = "6-5", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "Extranet_ASSURÉ_IRD - espace assuré", ProductId = 11, SegmentCode = "GROUPE", Code = "7-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "Extranet_ASSURÉ_IRD - pretty qualif", ProductId = 11, SegmentCode = "GROUPE", Code = "7-2", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "iTOP - portail collaborateur", ProductId = 12, SegmentCode = "GROUPE", Code = "8-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "myROADIA - socle", ProductId = 13, SegmentCode = "GROUPE", Code = "9-1", StartDate = DateTime.ParseExact("01/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "myROADIA - tunnel EAD", ProductId = 13, SegmentCode = "GROUPE", Code = "9-2", StartDate = DateTime.ParseExact("01/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "myROADIA - signature électronique", ProductId = 13, SegmentCode = "GROUPE", Code = "9-3", StartDate = DateTime.ParseExact("01/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "myROADIA - API Gouv Information société", ProductId = 13, SegmentCode = "GROUPE", Code = "9-4", StartDate = DateTime.ParseExact("01/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "myROADIA - orientation", ProductId = 13, SegmentCode = "GROUPE", Code = "9-5", StartDate = DateTime.ParseExact("01/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "SI_KPI - Kinitia", ProductId = 14, SegmentCode = "AUTO", Code = "10-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "SI_KPI - KPI Home", ProductId = 14, SegmentCode = "AUTO", Code = "10-2", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "SI_KPI - Assist Expert", ProductId = 14, SegmentCode = "AUTO", Code = "10-3", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "SI_KPI - Quarksup", ProductId = 14, SegmentCode = "AUTO", Code = "10-4", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "APPLICATION_SUPPORT_N2 - socle", ProductId = 15, SegmentCode = "GROUPE", Code = "17-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "BPM_ADENES - In", ProductId = 16, SegmentCode = "GROUPE", Code = "15-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "BPM_ADENES - Gestion Camunda", ProductId = 16, SegmentCode = "GROUPE", Code = "15-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "BPM_ADENES - Out", ProductId = 16, SegmentCode = "GROUPE", Code = "15-3", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "BPM_ADENES - Communication SMS", ProductId = 16, SegmentCode = "GROUPE", Code = "15-4", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "PUSH_BOT - Push rapport Sinapps", ProductId = 17, SegmentCode = "IRD France", Code = "11-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "PUSH_BOT - Push synthèse", ProductId = 17, SegmentCode = "IRD France", Code = "11-2", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "PUSH_BOT - Push facturation (gen. + push)", ProductId = 17, SegmentCode = "IRD France", Code = "11-3", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "PUSH_BOT - IPV", ProductId = 17, SegmentCode = "IRD France", Code = "11-4", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "PUSH_BOT - REC", ProductId = 17, SegmentCode = "IRD France", Code = "11-5", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "PUSH_BOT - rapport IRDweb", ProductId = 17, SegmentCode = "IRD France", Code = "11-6", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "PUSH_BOT - push ISD", ProductId = 17, SegmentCode = "IRD France", Code = "11-7", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "PUSH_BOT - Alerte instruction", ProductId = 17, SegmentCode = "IRD France", Code = "11-9", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "CONNECTEUR_CLIENT - socle", ProductId = 18, SegmentCode = "IRD France", Code = "20-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELENA - socle", ProductId = 19, SegmentCode = "IRD France", Code = "18-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ELLO_CONFIG - socle", ProductId = 20, SegmentCode = "IRD France", Code = "26-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_CONFIG - Annuaire entreprise", ProductId = 20, SegmentCode = "IRD France", Code = "26-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },

                new ModuleDao { Label = "MILOS - socle", ProductId = 21, SegmentCode = "IRD France", Code = "19-2", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "DOC_SCAN - Extraction de données", ProductId = 22, SegmentCode = "GROUPE", Code = "27-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "DOC_SCAN - Catégorisation", ProductId = 22, SegmentCode = "GROUPE", Code = "27-2", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "DOC_SCAN - OCR", ProductId = 22, SegmentCode = "GROUPE", Code = "27-3", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "DOC_SCAN - Détection fraude", ProductId = 22, SegmentCode = "GROUPE", Code = "27-4", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ELLO_SATISFACTION - socle", ProductId = 23, SegmentCode = "GROUPE", Code = "30-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },

                new ModuleDao { Label = "ELLO-SIGN - web", ProductId = 24, SegmentCode = "GROUPE", Code = "28-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO-SIGN - API sig", ProductId = 24, SegmentCode = "GROUPE", Code = "28-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },

                new ModuleDao { Label = "ROBOT_GED - Robot GED 3C", ProductId = 25, SegmentCode = "GROUPE", Code = "29-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ROBOT_GED - Robot GED ELEX", ProductId = 26, SegmentCode = "GROUPE", Code = "29-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "TO_ELLO - socle", ProductId = 27, SegmentCode = "GROUPE", Code = "38-1", StartDate = DateTime.ParseExact("01/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ELLO AUTO - socle", ProductId = 28, SegmentCode = "AUTO", Code = "31-1", StartDate = DateTime.ParseExact("01/02/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO AUTO - Sinapps Auto", ProductId = 28, SegmentCode = "AUTO", Code = "31-2", StartDate = DateTime.ParseExact("01/02/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ELLO_ADJUSTERS - socle", ProductId = 29, SegmentCode = "GROUPE", Code = "4-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "ELLO_HONO - socle", ProductId = 30, SegmentCode = "GROUPE", Code = "5-1", StartDate = DateTime.ParseExact("01/03/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                new ModuleDao { Label = "ELLO_INVOICE - socle", ProductId = 31, SegmentCode = "GROUPE", Code = "35-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture)},
                
                new ModuleDao { Label = "ELLO_AUDIT - Legacy", ProductId = 32, SegmentCode = "IRD France", Code = "33-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_AUDIT - Découplé", ProductId = 32, SegmentCode = "IRD France", Code = "33-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ELLO_PLANNER - Assistant RDV legacy", ProductId = 33, SegmentCode = "IRD France", Code = "32-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_PLANNER - Idigo", ProductId = 33, SegmentCode = "IRD France", Code = "32-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_PLANNER - Claims IA", ProductId = 33, SegmentCode = "IRD France", Code = "32-3", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_PLANNER - Standalone", ProductId = 33, SegmentCode = "IRD France", Code = "32-4", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_PLANNER - Standalone - Paramétrage compétences et planning", ProductId = 33, SegmentCode = "IRD France", Code = "32-5", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_PLANNER - Standalone - Module admin", ProductId = 33, SegmentCode = "IRD France", Code = "32-6", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "APPLICATION_ADENES - socle", ProductId = 34, SegmentCode = "IRD International", Code = "3-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ELLO_WORLD - socle", ProductId = 35, SegmentCode = "IRD International", Code = "34-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_WORLD_CONFIG - socle", ProductId = 36, SegmentCode = "IRD International", Code = "39-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_WORLD_CONFIG - endpoint", ProductId = 36, SegmentCode = "IRD International", Code = "39-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },

                new ModuleDao { Label = "HERMES_CORP - socle", ProductId = 37, SegmentCode = "GROUPE", Code = "39-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "HERMES_CORP - Endpoint", ProductId = 37, SegmentCode = "GROUPE", Code = "39-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                    
                new ModuleDao { Label = "ELLO EXPERT - socle", ProductId = 38, SegmentCode = "IRD France", Code = "22-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "ELLO_GED  - socle", ProductId = 39, SegmentCode = "IRD France", Code = "23-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_GED  - Robot GED", ProductId = 39, SegmentCode = "IRD France", Code = "23-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_GED  - Recherche documentaire", ProductId = 39, SegmentCode = "IRD France", Code = "23-3", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },

                new ModuleDao { Label = "ELLO_YOU - Ouverture", ProductId = 40, SegmentCode = "IRD France", Code = "21-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_YOU - Rendez-vous", ProductId = 40, SegmentCode = "IRD France", Code = "21-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ELLO_YOU - Transverse", ProductId = 40, SegmentCode = "IRD France", Code = "21-3", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },

                new ModuleDao { Label = "HERMES_FR - socle", ProductId = 41, SegmentCode = "IRD France", Code = "24-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },

                new ModuleDao { Label = "ULYSSE - Construction", ProductId = 42, SegmentCode = "IRD France", Code = "25-1", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ULYSSE - Dommage", ProductId = 42, SegmentCode = "IRD France", Code = "25-2", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "ULYSSE - Transverse ", ProductId = 42, SegmentCode = "IRD France", Code = "25-3", StartDate = DateTime.ParseExact("01/01/2025T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "Support", ProductId = 43, SegmentCode = "DSE", Code = "99-99", StartDate = DateTime.ParseExact("01/01/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                
                new ModuleDao { Label = "E-AGORA", ProductId = 40, SegmentCode = "DSE", Code = "21-4", StartDate = DateTime.ParseExact("01/03/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },
                new ModuleDao { Label = "E-AGORA", ProductId = 19, SegmentCode = "DSE", Code = "19-2", StartDate = DateTime.ParseExact("01/03/2026T00:00:00", "dd/MM/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture) },

            };

            context.Modules.AddRange(modules);
            await context.SaveChangesAsync();
        }
        
        if (!context.Activity.Any())
        {
            var activities = new List<ActivityDao>
            {
                new ActivityDao { Label = "BUILD", Code = "1"},
                new ActivityDao { Label = "RUN" , Code = "2"},
                new ActivityDao { Label = "Comitologies et Evenement exceptionnel DSE", Code = "3" },
                new ActivityDao { Label = "FOR- Formation", Code = "4" },
                new ActivityDao { Label = "ABS -  Absence (congés / maladie…)", Code = "5" }
            };

            context.Activity.AddRange(activities);
            await context.SaveChangesAsync();
        }

    }
    
}