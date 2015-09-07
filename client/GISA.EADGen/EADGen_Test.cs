using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Schema;

using NUnit.Framework;

using GISA.Model;

namespace GISA.EADGen {
    class EADGen_Test {

        public static void Main() {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try {
                string fileName = "GISA_EAD_Test.xml";
                EADGenerator gen_to_test = new EADGenerator(fileName, ho.Connection);
                //long idNivel = 100317;
                //long idNivel = 1865;
                //long idNivel = 23;

                // GISA_CS7_FEUP:
                //long idNivel = 14;    // Pautas ...
                //long idNivel = 212;     // Secretaria
                //long idNivel = 178;     // Conselho directivo
                //long IDNivel_PAI = 214;
                //long idNivel = 215;     // Direcao de servicos academicos...
                long IDNivel_PAI = 220;     // FEUP
                long idNivel = 222;
                //long IDNivel_PAI = 19;     // FEUP
                //long idNivel = 52416;
                //long idNivel = 219;     // Seccao de pessoal
                //long idNivel = 168;     // (Serie) Comissao cooordenadora...

                //long idNivel = 102027;      // Para imagens:

                // GISA_CS6_CMGaia: 48326; 50600
                //long idNivel = 50066;   // Secretaria
                //long idNivel = 50570;   // Orlando Miranda
                //long idNivel = 46139;       // Presidência. 2002-2008

                gen_to_test.generate(IDNivel_PAI, idNivel);
                validate(fileName);
            }
            finally {
                ho.Dispose();
            }

        }

        [Test] public void validate_generated_ead() {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try {
                string fileName = "GISA_EAD_Test.xml";
                EADGenerator gen_to_test = new EADGenerator(fileName, ho.Connection);
                long IDNivel_PAI = 214;
                long idNivel = 215;
                gen_to_test.generate(IDNivel_PAI, idNivel);
                validate(fileName);
                Assert.IsTrue(isValid);
            }
            finally {
                ho.Dispose();
            }
        }

        private static bool isValid = true; 

        private static void validate(string fileName) {
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ValidationType = ValidationType.DTD;
            readerSettings.ValidationEventHandler += new ValidationEventHandler(theValidationEventHandler);

            using (XmlReader reader = XmlReader.Create(fileName, readerSettings)) {
                while (reader.Read()) {
                    // ??? writeln ???
                }
            }

            if (isValid)
                Console.WriteLine("Document is valid");
            else
                Console.WriteLine("Document is invalid");
        }

        public static void theValidationEventHandler(object sender,  ValidationEventArgs args) {
           isValid = false;
           Console.WriteLine("Validation event:\n" + args.Message);
        }

    }
}
