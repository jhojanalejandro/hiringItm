﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiHiringItm.CONTEXT.Context;
using WebApiHiringItm.CORE.Core.ExportToExcel.Interfaces;
using WebApiHiringItm.MODEL.Dto.ContratoDto;
using WebApiHiringItm.MODEL.Dto;
using Microsoft.EntityFrameworkCore;
using Color = System.Drawing.Color;

namespace WebApiHiringItm.CORE.Core.ExportToExcel
{
    public class ExportToExcelCore : IExportToExcelCore
    {
        #region Dependency
        private readonly HiringContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ExportToExcelCore(HiringContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region PUBLIC METHODS
        public async Task<MemoryStream> ExportToExcelCdp(Guid ContractId)
        {

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("DAP");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 1;
                var row = startRow;
                worksheet.Cells["A1"].Value = "IdentificadorBaseDedatos";
                worksheet.Cells["B1"].Value = "Consecutivo";
                worksheet.Cells["C1"].Value = "Convenio";
                worksheet.Cells["D1"].Value = "Nombre Contratista";
                worksheet.Cells["E1"].Value = "Cedula";
                worksheet.Cells["F1"].Value = "Objeto";
                worksheet.Cells["G1"].Value = "Duración";
                worksheet.Cells["H1"].Value = "Valor Contrato";
                worksheet.Cells["I1"].Value = "Elemento";
                worksheet.Cells["J1"].Value = "Obligaciones";
                worksheet.Cells["K1"].Value = "CDP";
                worksheet.Cells["L1"].Value = "Número Contrato";
                worksheet.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 232, 230));
                worksheet.Cells["A1:P1"].Style.Font.Bold = true;
                worksheet.Cells["A1:P1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:P1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:P1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:P1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                row = 2;
                var data = _context.DetailProjectContractor
                            .Include(x => x.Contractor)
                            .Include(x => x.Element)
                            .Include(x => x.HiringData)
                            .Include(x => x.Component)
                            .Include(x => x.Contract)
                            .Where(x => x.ContractId == ContractId);
                var dataList = data.Select(w => new DetailProjectContractorDto()
                {
                    ContractorId = w.ContractorId.ToString(),
                    NombreComponente = w.Component.NombreComponente,
                    Nombre = w.Contractor.Nombre + " " + w.Contractor.Apellido,
                    Identificacion = w.Contractor.Identificacion,
                    ObjetoConvenio = w.Element.ObjetoElemento,
                    ValorTotal = w.Contractor.EconomicdataContractor.Where(w => w.ContractId.Equals(ContractId)).Select(s => s.TotalValue).FirstOrDefault(),
                    NombreElemento = w.Element.NombreElemento,
                    GeneralObligation = w.Element.ObligacionesGenerales,
                    SpecificObligation = w.Element.ObligacionesEspecificas,
                    InitialDate = w.HiringData.FechaRealDeInicio,
                    FinalDate = w.HiringData.FechaFinalizacionConvenio,
                    Convenio = w.Contract.ProjectName

                })
                .AsNoTracking()
                .ToList();
                int nro = 0;
                foreach (var user in dataList)
                {
                    if (user.InitialDate.HasValue && user.FinalDate.HasValue && user.NombreElemento != null && user.ValorTotal != null && user.ObjetoConvenio != null && user.NombreElemento != null)
                    {
                        nro++;
                        var durationContract = CalculateDateContract(user.InitialDate.Value, user.FinalDate.Value);
                        var unifyObligation = SeparateObligation(user.GeneralObligation, user.SpecificObligation);
                        worksheet.Cells[row, 1].Value = user.ContractorId;
                        worksheet.Cells[row, 2].Value = nro;
                        worksheet.Cells[row, 3].Value = user.Convenio;
                        worksheet.Cells[row, 4].Value = user.Nombre;
                        worksheet.Cells[row, 5].Value = user.Identificacion;
                        worksheet.Cells[row, 6].Value = user.ObjetoConvenio;
                        worksheet.Cells[row, 7].Value = durationContract;
                        worksheet.Cells[row, 8].Value = user.ValorTotal;
                        worksheet.Cells[row, 9].Value = user.NombreElemento;
                        worksheet.Cells[row, 10].Value = unifyObligation;
                        worksheet.Cells[row, 11].Value = "";
                        worksheet.Cells[row, 12].Value = "";
                        row++;
                    }
                }
                worksheet.Columns.AutoFit();

                xlPackage.Workbook.Properties.Title = "Lista de contratistas";
                xlPackage.Workbook.Properties.Author = "ITM";
                xlPackage.Workbook.Properties.Created = DateTime.Now;
                xlPackage.Workbook.Properties.Subject = "Lista de contratistas";

                xlPackage.Save();

            }
            stream.Position = 0;
            return await Task.FromResult(stream);
        }

        public async Task<MemoryStream> ExportContratacionDap(ControllerBase controller, Guid ContractId)
        {
            var data = _context.DetailProjectContractor
                .Include(x => x.Contractor)
                .Include(x => x.Element)
                    .ThenInclude(t => t.Cpc)
                .Include(x => x.HiringData)
                .Include(x => x.Component)
                .Include(x => x.Contract)
                .Where(x => x.ContractId == ContractId);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Hoja1");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 1;
                var row = startRow;
                worksheet.Cells["A1"].Value = "CONVENIO";
                worksheet.Cells["B1"].Value = "NOMBRE COMPLETO";
                worksheet.Cells["C1"].Value = "CPC";
                worksheet.Cells["D1"].Value = "CDP";
                worksheet.Cells["E1"].Value = "VALOR CONTRATO";
                worksheet.Cells["F1"].Value = "ACTA COMITÉ";
                worksheet.Cells["A1:F1"].AutoFilter = true;
                worksheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 232, 230));
                worksheet.Cells["A1:F1"].Style.Font.Bold = true;
                worksheet.Cells["A1:F1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:F1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:F1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:F1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                row = 2;
                var dataList = data.Select(w => new DetailProjectContractorDto()
                {
                    Convenio = w.Contract.ProjectName,
                    CompanyName = w.Contract.CompanyName,
                    NombreComponente = w.Component.NombreComponente,
                    Cpc = w.Element.Cpc.CpcName,
                    Nombre = w.Contractor.Nombre + " " + w.Contractor.Apellido,
                    Identificacion = w.Contractor.Identificacion,
                    ObjetoConvenio = w.Contract.ObjectContract,
                    ValorTotal = w.Element.ValorTotal,
                    NombreElemento = w.Element.NombreElemento,

                    Cdp = w.HiringData.Cdp,
                })
                .AsNoTracking()
                .ToList();
                foreach (var user in dataList)
                {
                    
                    if (user.ValorTotal != null && user.Cpc != null && user.Cdp != null)
                    {
                        worksheet.Cells[row, 1].Value = user.Convenio;
                        worksheet.Cells[row, 2].Value = user.Nombre;
                        worksheet.Cells[row, 3].Value = user.Cpc;
                        worksheet.Cells[row, 4].Value = user.Cdp;
                        worksheet.Cells[row, 5].Value = user.ValorTotal;
                        worksheet.Cells[row, 6].Value = "";
                        row++;

                    }
                }
                worksheet.Columns.AutoFit();
                xlPackage.Workbook.Properties.Title = "Solicitud contratación DAP";
                xlPackage.Workbook.Properties.Author = "Maicol Yepes";
                xlPackage.Workbook.Properties.Created = DateTime.Now;
                xlPackage.Workbook.Properties.Subject = "Solicitud contratación DAP";
                xlPackage.Save();
            }
            stream.Position = 0;
            return await Task.FromResult(stream);
        }

        public async Task<MemoryStream> ExportCdp(ControllerBase controller, Guid ContractId)
        {
            // Get the user list 
            var data = _context.DetailProjectContractor.Where(x => x.ContractId == ContractId)
                .Include(x => x.Contractor)
                .Include(x => x.Element)
                    .ThenInclude(t => t.Cpc)
                .Include(x => x.HiringData)
                .Include(x => x.Component)
                .Include(x => x.Contract);

            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Hoja1");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 1;
                var row = startRow;

                worksheet.Cells["A1"].Value = "CODIGO DE LA EMPRESA";
                worksheet.Cells["B1"].Value = "CONSECUTIVO";
                worksheet.Cells["C1"].Value = "CODIGO TIPO DE OPERACIÓN";
                worksheet.Cells["D1"].Value = "NÚMERO DEL DOCUMENTO";
                worksheet.Cells["E1"].Value = "FECHA MOVIMIENTO";
                worksheet.Cells["F1"].Value = "CODIGO DE LA SUCURSAL";
                worksheet.Cells["G1"].Value = "DESCRIPCIÓN DEL MOVIMIENTO";
                worksheet.Cells["H1"].Value = "CODIGO DEL TERCERO";
                worksheet.Cells["I1"].Value = "DOCUMENTO DEL SUPERVISOR";
                worksheet.Cells["J1"].Value = "CLASE DE DOCUMENTO";
                worksheet.Cells["K1"].Value = "TIPO DE DOCUMENTO SOPORTE";
                worksheet.Cells["L1"].Value = "NÚMERO DE DOCUMENTO SOPORTE";
                worksheet.Cells["M1"].Value = "NÚMERO DEL SIIF";
                worksheet.Cells["N1"].Value = "NÚMERO DEL MES ASOCIADO A LA FECHA";
                worksheet.Cells["O1"].Value = "NÚMERO DEL DÍA ASOCIADO A LA FECHA";
                worksheet.Cells["P1"].Value = "CODIGO DEL RUBRO";
                worksheet.Cells["Q1"].Value = "DESCRIPCIÓN DEL DETALLE DEL MOVIMIENTO ASOCIADO AL RUBRO";
                worksheet.Cells["R1"].Value = "CODIGO DEL CENTRO DE COSTOS";
                worksheet.Cells["S1"].Value = "CODIGO DEL PROYECTO";
                worksheet.Cells["T1"].Value = "CPC";
                worksheet.Cells["U1"].Value = "SIGNO";
                worksheet.Cells["V1"].Value = "VALOR ASOCIADO AL RUBRO";
                worksheet.Cells["W1"].Value = "CONSECUTIVO REGISTRO BASE";
                worksheet.Cells["X1"].Value = "CEDULA";
                worksheet.Cells["Y1"].Value = "NOMBRE";

                worksheet.Cells["A1:Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:Y1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 232, 230));
                worksheet.Cells["A1:Y1"].Style.Font.Bold = true;
                worksheet.Cells["A1:Y1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:Y1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:Y1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:Y1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
               
                row = 2;
                int nro = 0;
                var dataList = data.Select(w => new SolicitudCdpDto()
                {
                    Consecutivo = w.Element.Consecutivo,
                    NombreElemento = w.Element.NombreElemento,
                    NombreComponente = w.Component.NombreComponente,
                    NumeroConvenio = w.Contract.NumberProject,
                    CedulaSupervisor = w.HiringData.IdentificacionSupervisor,
                    NombreSupervisor = w.HiringData.SupervisorItm,
                    Rubro = w.Contract.Rubro,
                    Cpc = w.Element.Cpc.CpcName,
                    Projecto = w.Contract.Project,
                    Nombre = w.Contractor.Nombre,
                    Cedula = w.Contractor.Identificacion
                })
                .AsNoTracking()
                .ToList();
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                var day = DateTime.Now.Day;
                var date = DateTime.Now.ToString("dd/MM/yyyy");

                foreach (var user in dataList)
                {
                    if (user.Consecutivo != null && user.NombreElemento != null && user.NumeroConvenio != null && user.Cpc != null)
                    {
                        nro++;
                        worksheet.Cells[row, 1].Value = 108;
                        worksheet.Cells[row, 2].Value = nro;
                        worksheet.Cells[row, 3].Value = 49;
                        worksheet.Cells[row, 4].Value = nro;
                        worksheet.Cells[row, 5].Value = date;
                        worksheet.Cells[row, 6].Value = 1;
                        worksheet.Cells[row, 7].Value = user.NumeroConvenio + "/" + year + user.NombreElemento + "("+user.Consecutivo+")";
                        worksheet.Cells[row, 8].Value = 800214750;
                        worksheet.Cells[row, 9].Value = user.CedulaSupervisor;
                        worksheet.Cells[row, 10].Value = "SCDP";
                        worksheet.Cells[row, 11].Value = 0;
                        worksheet.Cells[row, 12].Value = 0;
                        worksheet.Cells[row, 13].Value = 0;
                        worksheet.Cells[row, 14].Value = month;
                        worksheet.Cells[row, 15].Value = day;
                        worksheet.Cells[row, 16].Value = user.NumeroConvenio + "/" + year + user.NombreElemento + "(" + user.Consecutivo + ")";
                        worksheet.Cells[row, 17].Value = user.Rubro;
                        worksheet.Cells[row, 18].Value = "1016231102123";
                        worksheet.Cells[row, 19].Value = user.Projecto;
                        worksheet.Cells[row, 20].Value = user.Cpc;
                        worksheet.Cells[row, 21].Value = "S";
                        worksheet.Cells[row, 22].Value = user.Rubro;
                        worksheet.Cells[row, 23].Value = 0;
                        worksheet.Cells[row, 24].Value = user.Cedula;
                        worksheet.Cells[row, 25].Value = user.Nombre;

                        row++;
                    }
                }
                { }
                worksheet.Columns.AutoFit();
                xlPackage.Workbook.Properties.Title = "Solicitud CDP";
                xlPackage.Workbook.Properties.Author = "ITM";
                xlPackage.Workbook.Properties.Created = DateTime.Now;
                xlPackage.Workbook.Properties.Subject = "Solicitud CDP";
                xlPackage.Save();
            }
            stream.Position = 0;
            return await Task.FromResult(stream);
        }

        public async Task<MemoryStream> ExportSolicitudPaa(ControllerBase controller, Guid ContractId)
        {

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Adquisiciones");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;
                //Create Headers and format them
                //worksheet.Cells["A1"].Value = "Con el fin de proceder a completar las columnas: Código UNSPSC, Duración del contrato (intervalo: días, meses, años), Modalidad de selección, Fuente de los recursos, ¿Se requieren vigencias futuras?, Estado de solicitud de vigencias futuras; vea la " + " Hoja de soporte " + " para saber cuáles son los códigos que aplican a cada columna.";
                using (var r = worksheet.Cells["A1:Q3"])
                {
                    r.Style.WrapText = true;
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.Black);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 232, 230));
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                }
                worksheet.Cells["A4"].Value = "Código UNSPSC";
                worksheet.Cells["B4"].Value = "Descripción";
                worksheet.Cells["C4"].Value = "Fecha estimada de inicio de proceso de selección";
                worksheet.Cells["D4"].Value = "Fecha estimada de presentación de ofertas";
                worksheet.Cells["E4"].Value = "Duración del contrato";
                worksheet.Cells["F4"].Value = "Duración del contrato";
                worksheet.Cells["G4"].Value = "Modalidad de selección ";
                worksheet.Cells["H4"].Value = "Fuente de los recursos";
                worksheet.Cells["I4"].Value = "Valor total estimado";
                worksheet.Cells["J4"].Value = "Valor estimado en la vigencia actual";
                worksheet.Cells["K4"].Value = "¿Se requieren vigencias futuras?";
                worksheet.Cells["L4"].Value = "Estado de solicitud de vigencias futuras";
                worksheet.Cells["M4"].Value = "Unidad de contratación";
                worksheet.Cells["N4"].Value = "Ubicación";
                worksheet.Cells["O4"].Value = "Nombre del responsable";
                worksheet.Cells["P4"].Value = "Teléfono del responsable ";
                worksheet.Cells["R4"].Value = "Correo electrónico del responsable";
                worksheet.Cells["R4"].Value = "Proyecto de inversión";
                worksheet.Cells["S4"].Value = "DOCUMENTO";
                worksheet.Cells["T4"].Value = "NOMBRE";
                worksheet.Cells["U4"].Value = "HONORARIOS 2022";
                worksheet.Cells["V4"].Value = "FECHA REQUERIDA DE INICIO PIMER SEMESTRE";
                worksheet.Cells["W4"].Value = "FECHA TERMINACIÓN PRIMER CONTRATO";
                worksheet.Cells["X4"].Value = "DURACION";
                worksheet.Cells["Y4"].Value = "TOTAL CONTRATO";
                worksheet.Cells["Z4"].Value = "¿Debe cumplir con invertir mínimo el 30% de los recursos del presupuesto destinados a comprar alimentos, cumpliendo con lo establecido en la Ley 2046 de 2020, reglamentada por el Decreto 248 de 2021?";
                worksheet.Cells["AA4"].Value = "¿El contrato incluye el suministro de bienes y servicios distintos a alimentos?";

                var data = _context.DetailProjectContractor.Where(x => x.ContractId == ContractId)
                                    .Include(x => x.Contractor)
                                        .ThenInclude(i => i.EconomicdataContractor)
                                    .Include(x => x.Element)
                                    .Include(x => x.HiringData)
                                    .Include(x => x.Component)
                                    .Include(x => x.Contract);

                var dataList = data.Select(w => new DetailProjectContractorDto()
                {
                    Convenio = w.Contract.ProjectName,
                    CompanyName = w.Contract.CompanyName,
                    NombreComponente = w.Component.NombreComponente,
                    Nombre = w.Contractor.Nombre + " " + w.Contractor.Apellido,
                    Identificacion = w.Contractor.Identificacion,
                    ObjetoConvenio = w.Element.ObjetoElemento,
                    ValorTotal = w.Contractor.EconomicdataContractor.Where(w => w.ContractId.Equals(ContractId)).Select(s => s.TotalValue).FirstOrDefault(),
                    InitialDate = w.HiringData.FechaRealDeInicio,
                    FinalDate = w.HiringData.FechaFinalizacionConvenio,
                    User = w.Contractor.User.UserName,
                    Email = w.Contractor.User.UserEmail,
                    UnitValue = w.Contractor.EconomicdataContractor.Where(w => w.ContractId.Equals(ContractId)).Select(s => s.UnitValue).FirstOrDefault()

                })
                .AsNoTracking()
                .ToList();
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                var day = DateTime.Now.Day;
                var date = DateTime.Now.ToString("dd/MM/yyyy");
                row = 2;
                int nro = 0;
                foreach (var user in dataList)
                {
                    if ( user.ObjetoConvenio != null && user.Cpc != null && user.ValorTotal != null && user.InitialDate.HasValue && user.FinalDate.HasValue)
                    {
                        var durationContract = CalculateDateContract(user.InitialDate.Value, user.FinalDate.Value);
                        nro++;
                        worksheet.Cells[row, 1].Value = 80111600;
                        worksheet.Cells[row, 2].Value = user.ObjetoConvenio;
                        worksheet.Cells[row, 3].Value = month;
                        worksheet.Cells[row, 4].Value = month;
                        worksheet.Cells[row, 5].Value = 0;
                        worksheet.Cells[row, 6].Value = durationContract;
                        worksheet.Cells[row, 7].Value = "CCE-16";
                        worksheet.Cells[row, 8].Value = 0;
                        worksheet.Cells[row, 9].Value = user.ValorTotal;
                        worksheet.Cells[row, 10].Value = user.ValorTotal;
                        worksheet.Cells[row, 11].Value = 0;
                        worksheet.Cells[row, 12].Value = 0;
                        worksheet.Cells[row, 13].Value = "Unidad estratégica de negociosos";
                        worksheet.Cells[row, 14].Value = "CO-ANT-05001";
                        worksheet.Cells[row, 15].Value = user.User;
                        worksheet.Cells[row, 16].Value = 4405100;
                        worksheet.Cells[row, 17].Value = user.Email;
                        worksheet.Cells[row, 17].Value = "Convenios-funcionamiento";
                        worksheet.Cells[row, 18].Value = user.Identificacion;
                        worksheet.Cells[row, 19].Value = user.Nombre;
                        worksheet.Cells[row, 20].Value = user.UnitValue;
                        worksheet.Cells[row, 20].Value = user.InitialDate;
                        worksheet.Cells[row, 20].Value = user.FinalDate;
                        worksheet.Cells[row, 20].Value = durationContract;
                        worksheet.Cells[row, 20].Value = user.ValorTotal;
                        worksheet.Cells[row, 20].Value = 0;
                        worksheet.Cells[row, 20].Value = 0;
                        row++;
                    }
                 
                }
                if (nro > 0)
                {
                    worksheet.Columns.AutoFit();
                    xlPackage.Workbook.Properties.Title = "Solicitud PAA";
                    xlPackage.Workbook.Properties.Author = "ITM";
                    xlPackage.Workbook.Properties.Created = DateTime.Now;
                    xlPackage.Workbook.Properties.Subject = "Solicitud PAA";
                    xlPackage.Save();
                }
                else
                {
                    return null;
                }

            }
            stream.Position = 0;
            return await Task.FromResult(stream);
        }
        #endregion

        #region PRIVATE METHODS

        private string CalculateDateContract(DateTime initialDate, DateTime finalDate)
        {

            TimeSpan diferencia = finalDate - initialDate;

            int totalDias = (int)diferencia.TotalDays;
            int totalMeses = (int)(diferencia.TotalDays / 30.436875); // Promedio de días por mes
            int totalAnios = (int)(diferencia.TotalDays / 365.25); // Promedio de días por año

            int dias = totalDias % 30; // Días restantes después de los meses completos
            int meses = totalMeses % 12; // Meses restantes después de los años completos
            if (totalDias > 0 && totalMeses > 0 && totalAnios > 0)
            {
                return $"{totalAnios} años, {meses} meses, {dias} días";
            }
            else if (totalDias > 0 && totalMeses > 0)
            {
                return $"{meses} meses, {dias} días";
            }
            else
            {
                return $"{dias} días";
            }

        }

        private string SeparateObligation(string generalObligation, string specificObligation)
        {
            string unifyObligation = null;
            var generalObligationList = generalObligation.Split("->");
            var specificObligationList = specificObligation.Split("->");

            foreach (var item in generalObligationList)
            {
                unifyObligation += item;
            }
            foreach (var item in specificObligationList)
            {
                unifyObligation += item;
            }
            return unifyObligation;
        }
        #endregion
    }
}
