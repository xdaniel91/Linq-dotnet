CREATE PROCEDURE [dbo].[ps_Vendas_Por_Cliente]
    @clienteId int =0
AS
BEGIN
    SELECT
    i.FaixaId,
    i.ItemNotaFiscalId,
    i.NotaFiscalId,
    i.PrecoUnitario,
    i.Quantidade,
    i.PrecoUnitario * i.Quantidade As Total,
    n.DataNotaFiscal,
    f.Nome
    FROM ItemNotaFiscal i
    INNER JOIN NotaFiscal n ON i.NotaFiscalId = n.NotaFiscalId
    INNER JOIN Faixa f ON f.FaixaId = i.FaixaId
    WHERE n.ClienteId = @clienteId
END