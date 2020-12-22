namespace NiksoftCore.ITCF.Service
{
    public class UserLegalForm
    {
        public int Id { get; set; }
        public bool IsRegisteredCom { get; set; }
        public string RegDesc { get; set; }
        public bool HasBusinessCard { get; set; }
        public string BCardDesc { get; set; }
        public bool HasEconomicCode { get; set; }
        public string EconomicCodeDesc { get; set; }
        public bool HasExportExp { get; set; }
        public string ExportExpDesc { get; set; }
        public bool IsMiddleman { get; set; }
        public string MiddlemanDesc { get; set; }
        public string Certifications { get; set; }
        public string ProductionCapacity { get; set; }
        public bool HasBrand { get; set; }
        public string BrandDesc { get; set; }
        public string ExportCountries { get; set; }
        public bool HasCoBankAccount { get; set; }
        public string CoBankAccountDesc { get; set; }
        public bool HasOtherResidence { get; set; }
        public string OtherResidenceDesc { get; set; }
        public bool HasRegBrandOther { get; set; }
        public string RegBrandOtherDesc { get; set; }
        public bool IsFaBrand { get; set; }
        public string FaBrandDesc { get; set; }
        public bool HasWebsite { get; set; }
        public string WebsiteDesc { get; set; }
        public bool IsInternalProd { get; set; }
        public string InternalProdDesc { get; set; }
        public bool HasCalcStandards { get; set; }
        public string CalcStandardsDesc { get; set; }
        public bool HasExportLicense { get; set; }
        public string ExportLicenseDesc { get; set; }
        public bool HasReqInternalBranding { get; set; }
        public string ReqInternalBrandingDesc { get; set; }
        public bool IntExternalInves { get; set; }
        public string ExternalInvesDesc { get; set; }
        public bool IntVisitFactories { get; set; }
        public string VisitFactoriesDesc { get; set; }
        public bool CanVisitYouFactory { get; set; }
        public string VisitYouFactoryDesc { get; set; }
        public bool IntForeignMerchants { get; set; }
        public string ForeignMerchantsDesc { get; set; }
        public bool IsReqOperationLicense { get; set; }
        public string ReqOperationLicenseDesc { get; set; }
        public int UserId { get; set; }
    }
}
