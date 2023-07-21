namespace UI_BackOffice.Tools
{
    public static class HtmlString
    {
        public static string GetSignRequerimientoMateriales
        {
            get
            {
                return @"
                <!DOCTYPE html>
                <html lang='en'>
                  <head>
                    <meta charset='utf-8' />
                    <title>Requerimiento de Materiales</title>
                    <style>
                      * {
                        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                        font-size: 12px;
                      }
                      .tablew100 {
                        width: 100%;
                      }

                      .table_body td:nth-child(1),
                      .table_body td:nth-child(5),
                      .table_body td:nth-child(6),
                      .table_body td:nth-child(7) {
                        text-align: center;
                      }
                      .table_body thead th {
                        padding: 5px;
                        background-color: @Header_BackColor;
                        color: white;
                        font-size: 9px;
                      }
                      .table_body tbody td {
                        padding: 3px;
                        border-bottom: solid lightgray 1px;
                        font-size: 9px;
                      }
                      .firma {
                        text-align: center;
                        padding-top: 80px;
                      }
                      .firma table {
                        width: 100%;
                      }
                      .head {
                        padding: 10px;
                      }
                      .head table {
                        width: 100%;
                      }
                      .head table th {
                        border: solid lightgray 1px;
                        padding: 4px;
                      }
                      .dsc_line{
                        border-bottom: dashed 1px rgb(175, 173, 173);
                      }
                    </style>
                  </head>
                  <body>
                    <div class='head'>
                      <table>
                        <thead>
                          <tr>
                            <th rowspan='3' style='width: 120px'>
                              <img
                                src='@logotipo'
                                alt='logo'
                                width='100px'
                              />
                            </th>
                            <th style='width: 2px; border: none'></th>
                            <th rowspan='3' style='font-size: 12px'>
                              REQUERIMIENTO DE MATERIALES
                            </th>
                            <th style='width: 60px;font-size: 9px'>Código</th>
                            <th style='width: 120px;font-size: 9px'>@Codigo_ISO</th>
                          </tr>
                          <tr>
                            <th style='border: none'></th>
                            <th style='font-size: 9px'>Versión</th>
                            <th style='font-size: 9px'>@Version_ISO</th>
                          </tr>
                          <tr>
                            <th style='border: none'></th>
                            <th style='font-size: 9px'>Fecha</th>
                            <th style='font-size: 9px'>@Fecha_ISO</th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
                            <td>N° REQUERIMIENTO</td>
                            <td>:</td>
                            <td colspan='3' class='dsc_line'>@Num_Requerimiento</td>
                          </tr>
                          <tr>
                            <td>FECHA:</td>
                            <td>:</td>
                            <td colspan='3' class='dsc_line'>@Fecha_Requerimiento</td>
                          </tr>
                          <tr>
                            <td>CLIENTE/SEDE</td>
                            <td>:</td>
                            <td colspan='3' class='dsc_line'>@Cliente_Sede</td>
                          </tr>
                          <tr>
                            <td>SUPERVISOR SOLICITA</td>
                            <td>:</td>
                            <td colspan='3' class='dsc_line'>@Supervisor_Solicitante</td>
                          </tr>
                          <tr>
                            <td>OPERARIO SOLICITA</td>
                            <td>:</td>
                            <td colspan='3' class='dsc_line'>@Operario_Solicitante</td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                    <table class='table_body tablew100'>
                      <thead>
                        <tr>
                          <th style='width: 26px'>ITEM</th>
                          <th>TIPO DE SUMINISTRO</th>
                          <th style='width: 68px'>CÓDIGO PROD.</th>
                          <th>DESCRIPCIÓN DE MATERIAL</th>
                          <th style='width: 42px'>TIPO DE MEDIDA</th>
                          <th style='width: 48px'>STOCK ACTUAL</th>
                          <th style='width: 48px'>PEDIDO DEL MES</th>
                          <th>OBSERVACIÓN</th>
                        </tr>
                      </thead>
                      <tbody>
                        @rows
                      </tbody>
                    </table>
                    <div class='firma'>
                      <table>
                        <thead>
                          <tr>
                            <th>_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _</th>
                            <th>&nbsp;</th>
                            <th>_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _</th>
                            <th>&nbsp;</th>
                            <th>_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _</th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
                            <td>V°B° OPERARIO</td>
                            <td>&nbsp;</td>
                            <td>V°B° SUPERVISOR GENERAL</td>
                            <td>&nbsp;</td>
                            <td>V°B° LOGÍSTICA</td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </body>
                </html>
                ";
            }
        }

    }
}

//  linea 124: style='width: 60px'

