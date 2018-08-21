using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class PersonnelModel
    {
        public string Code { get; set; }
        public string CompareCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public int? PlaceOfBirthId { get; set; }
        public int? FatherHometownId { get; set; }
        public int? NationalityId { get; set; }
        public int? ReligionId { get; set; }
        public int? EthnicId { get; set; }
        public string IdNumber { get; set; }
        public int? IdIssuePlaceId { get; set; }
        public DateTime? IdIssueDate { get; set; }
        public string IdcNumber { get; set; }
        public int? IdcIssuePlaceId { get; set; }
        public DateTime? IdcIssueDate { get; set; }
        public int? MajorLevelId { get; set; }
        public int? StudyLevelId { get; set; }
        public string Phone { get; set; }
        public string HometownAddress { get; set; }
        public int? HometownPrecinctId { get; set; }
        public int? HometownDistrictId { get; set; }
        public int? HometownProvinceId { get; set; }
        public string CurrentAddress { get; set; }
        public int? CurrentPrecinctId { get; set; }
        public int? CurrentDistrictId { get; set; }
        public int? CurrentProvinceId { get; set; }
        public string TaxCode { get; set; }
        public DateTime? BeginEmployDate { get; set; }
        public DateTime? EndEmployDate { get; set; }
        public string ContractCode { get; set; }
        public int? WaterBodyId { get; set; }
        public int? PositionId { get; set; }
        public int? CareerTitleId { get; set; }
    }
}
