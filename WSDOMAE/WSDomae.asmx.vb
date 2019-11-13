Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Globalization
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Configuration
Imports System.Reflection
Imports System.IO
Imports System.IO.MemoryStream
Imports System.Text



' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://gudanggaramtbk.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Service1
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function Get_Test_Result() As String
        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd As OracleCommand
        Dim Result As String
        Dim XResult As OracleParameter

        Result = "v.1.4"
        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)

        Try
            OraDbcon.Open()
            cmd = New OracleCommand()

            XResult = New OracleParameter
            With XResult
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Output
                .ParameterName = "xresult"
            End With

            With cmd
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.GET_TEST_RESULT"
                .Parameters.Add(XResult)
                .BindByName = True
                .ExecuteNonQuery()
                Result = XResult.Value
            End With
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try
        Return Result
    End Function


    <WebMethod()> _
    Public Function Request_PostProcess(ByVal p_oracle_header_id As String) As String
        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd As OracleCommand
        Dim Result As String

        Dim xp_p_errbuf As OracleParameter
        Dim xp_p_retcode As OracleParameter
        Dim xp_p_oracle_header_id As OracleParameter


        Result = " "
        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)

        Try
            OraDbcon.Open()
            cmd = New OracleCommand()

            '============== p_errbuf ===========
            xp_p_errbuf = New OracleParameter
            With xp_p_errbuf
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Output
                .ParameterName = "errbuf"
            End With

            '============== p_errbuf ===========
            xp_p_retcode = New OracleParameter
            With xp_p_retcode
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Output
                .ParameterName = "retcode"
            End With


            '============== do header id oracle ===========
            xp_p_oracle_header_id = New OracleParameter
            With xp_p_oracle_header_id
                .OracleDbType = OracleDbType.Int64
                .Value = p_oracle_header_id
                .Direction = ParameterDirection.Input
                .ParameterName = "p_do_header_id"
            End With

            With cmd
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.RUN_CONCURRENT"
                .Parameters.Add(xp_p_errbuf)
                .Parameters.Add(xp_p_retcode)
                .Parameters.Add(xp_p_oracle_header_id)
                .BindByName = True
                .ExecuteNonQuery()

            End With
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try
        Return Result
    End Function

    <WebMethod()> _
Public Function Request_Flag_list_loading(ByVal p_no_kendaraan As String, _
                                          ByVal p_rak_persiapan As String, _
                                          ByVal p_bin_persiapan As String, _
                                          ByVal p_no_trx As String) As String
        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd As OracleCommand
        Dim Result As String

        Dim fll_p_no_kendaraan As OracleParameter
        Dim fll_p_rak_persiapan As OracleParameter
        Dim fll_p_bin_persiapan As OracleParameter
        Dim fll_p_no_trx As OracleParameter

        Result = " "
        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)

        Try
            OraDbcon.Open()
            cmd = New OracleCommand()

            '============== p_no_kendaraan ===========
            fll_p_no_kendaraan = New OracleParameter
            With fll_p_no_kendaraan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_no_kendaraan
                .ParameterName = "p_no_kendaraan"
            End With

            '============== p_rak_persiapan ===========
            fll_p_rak_persiapan = New OracleParameter
            With fll_p_rak_persiapan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_rak_persiapan
                .ParameterName = "p_rak_persiapan"
            End With

            '============== p_bin_persiapan ===========
            fll_p_bin_persiapan = New OracleParameter
            With fll_p_bin_persiapan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_bin_persiapan
                .ParameterName = "p_bin_persiapan"
            End With

            '=============== p_no_trx ================
            fll_p_no_trx = New OracleParameter
            With fll_p_no_trx
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_no_trx
                .ParameterName = "p_no_trx"
            End With

            With cmd
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.UPDATE_FLAG_LIST_LOADING"
                .Parameters.Add(fll_p_no_kendaraan)
                .Parameters.Add(fll_p_rak_persiapan)
                .Parameters.Add(fll_p_bin_persiapan)
                .Parameters.Add(fll_p_no_trx)

                .BindByName = True
                .ExecuteNonQuery()

            End With
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try
        Return Result
    End Function

    <WebMethod()> _
Public Function Get_List_Lookup() As DataTable
        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd_look As OracleCommand
        Dim da_look As OracleDataAdapter
        Dim ds_look As DataSet

        Dim XResult_look As OracleParameter

        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)
        ds_look = New DataSet

        Try
            OraDbcon.Open()
            cmd_look = New OracleCommand()

            XResult_look = New OracleParameter
            With XResult_look
                .OracleDbType = OracleDbType.RefCursor
                .Direction = ParameterDirection.Output
                .ParameterName = "p_recordset"
            End With

            With cmd_look
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.GET_MASTER_LOOKUP"
                .Parameters.Add(XResult_look)
                .BindByName = True
                .ExecuteNonQuery()
            End With

            Try
                da_look = New OracleDataAdapter(cmd_look)
                da_look.Fill(ds_look)
            Catch ex As Exception
            End Try
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try
        Return ds_look.Tables(0)
    End Function


    <WebMethod()> _
    Public Function Get_List_Employee() As DataTable
        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd As OracleCommand
        Dim da As OracleDataAdapter
        Dim ds As DataSet

        Dim XResult As OracleParameter

        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)
        ds = New DataSet

        Try
            OraDbcon.Open()
            cmd = New OracleCommand()

            XResult = New OracleParameter
            With XResult
                .OracleDbType = OracleDbType.RefCursor
                .Direction = ParameterDirection.Output
                .ParameterName = "p_recordset"
            End With

            With cmd
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.GET_MASTER_EMPLOYEE"
                .Parameters.Add(XResult)
                .BindByName = True
                .ExecuteNonQuery()
            End With

            Try
                da = New OracleDataAdapter(cmd)
                da.Fill(ds)
            Catch ex As Exception
            End Try
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try
        Return ds.Tables(0)
    End Function

    <WebMethod()> _
Public Function Get_List_RakPersiapan() As DataTable
        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd_rp As OracleCommand
        Dim da_rp As OracleDataAdapter
        Dim ds_rp As DataSet

        Dim XResult_rp As OracleParameter

        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)
        ds_rp = New DataSet

        Try
            OraDbcon.Open()
            cmd_rp = New OracleCommand()

            XResult_rp = New OracleParameter
            With XResult_rp
                .OracleDbType = OracleDbType.RefCursor
                .Direction = ParameterDirection.Output
                .ParameterName = "p_recordset"
            End With

            With cmd_rp
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.GET_LIST_RAK_PERSIAPAN"
                .Parameters.Add(XResult_rp)
                .BindByName = True
                .ExecuteNonQuery()
            End With

            Try
                da_rp = New OracleDataAdapter(cmd_rp)
                da_rp.Fill(ds_rp)
            Catch ex As Exception
            End Try
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try

        Return ds_rp.Tables(0)

    End Function

    <WebMethod()> _
Public Function Get_List_No_Trx(ByVal p_rak_persiapan As String) As DataTable
        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd_tx As OracleCommand
        Dim da_tx As OracleDataAdapter
        Dim ds_tx As DataSet

        Dim XResult_lo As OracleParameter
        Dim xRakPersiapan As OracleParameter

        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)
        ds_tx = New DataSet

        Try
            OraDbcon.Open()
            cmd_tx = New OracleCommand()

            xRakPersiapan = New OracleParameter
            With xRakPersiapan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_rak_persiapan
                .ParameterName = "p_rak_persiapan"
            End With

            XResult_lo = New OracleParameter
            With XResult_lo
                .OracleDbType = OracleDbType.RefCursor
                .Direction = ParameterDirection.Output
                .ParameterName = "p_recordset"
            End With

            With cmd_tx
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.GET_LIST_NO_TRX"
                .Parameters.Add(XResult_lo)
                .Parameters.Add(xRakPersiapan)
                .BindByName = True
                .ExecuteNonQuery()
            End With

            Try
                da_tx = New OracleDataAdapter(cmd_tx)
                da_tx.Fill(ds_tx)
            Catch ex As Exception
            End Try
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try

        Return ds_tx.Tables(0)

    End Function

    <WebMethod()> _
Public Function Get_List_Manifest_By_Trx( _
                              ByVal p_no_kendaraan As String, _
                              ByVal p_rak_persiapan As String, _
                              ByVal p_bin_persiapan As String, _
                              ByVal p_no_trx As String) As DataTable
        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd_no As OracleCommand
        Dim da_no As OracleDataAdapter
        Dim ds_no As DataSet
        Dim dt_no As DataTable

        Dim XResult_no As OracleParameter
        Dim XNoKendaraan As OracleParameter
        Dim XRakPersiapan As OracleParameter
        Dim XBinPersiapan As OracleParameter
        Dim XNoTrx As OracleParameter

        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)

        dt_no = New DataTable
        ds_no = New DataSet

        Try
            OraDbcon.Open()
            cmd_no = New OracleCommand()

            XNoKendaraan = New OracleParameter
            With XNoKendaraan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_no_kendaraan
                .ParameterName = "p_no_kendaraan"
            End With

            XRakPersiapan = New OracleParameter
            With XRakPersiapan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_rak_persiapan
                .ParameterName = "p_rak_persiapan"
            End With

            XBinPersiapan = New OracleParameter
            With XBinPersiapan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_bin_persiapan
                .ParameterName = "p_bin_persiapan"
            End With

            XNoTrx = New OracleParameter
            With XNoTrx
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_no_trx
                .ParameterName = "p_no_trx"
            End With

            XResult_no = New OracleParameter
            With XResult_no
                .OracleDbType = OracleDbType.RefCursor
                .Direction = ParameterDirection.Output
                .ParameterName = "p_recordset"
            End With

            With cmd_no
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.GET_LIST_MANIFEST_BY_TRX"
                .Parameters.Add(XResult_no)
                .Parameters.Add(XNoKendaraan)
                .Parameters.Add(XRakPersiapan)
                .Parameters.Add(XBinPersiapan)
                .Parameters.Add(XNoTrx)
                .BindByName = True
                .ExecuteNonQuery()
            End With

            Try
                da_no = New OracleDataAdapter(cmd_no)
                da_no.Fill(ds_no)
            Catch ex As Exception
            End Try
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try

        If (ds_no.Tables(0) Is Nothing) Then

        End If

        dt_no = ds_no.Tables(0)

        Return dt_no

    End Function

    <WebMethod()> _
    Public Function Backup_DataBase(ByVal p_device_id As String, _
                                    ByVal p_dbcontent As String) As String
        Dim ImageDataRaw As Byte()
        Dim ms As MemoryStream
        Dim fs As FileStream
        Dim result As String
        Dim timestamp As String
        ImageDataRaw = Convert.FromBase64String(p_dbcontent)

        timestamp = Now.ToString("yyyyMMddHHmmssFFF")

        ms = New MemoryStream(ImageDataRaw, 0, ImageDataRaw.Length)
        fs = New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/db/") + p_device_id + "_" + timestamp + ".db", FileMode.Create)
        If (fs.Length > 0) Then
            result = "File Created"
        Else
            result = "File Not Created"
        End If
        ms.WriteTo(fs)
        ms.Close()
        fs.Close()
        fs.Dispose()
        Return result
    End Function

    <WebMethod()> _
Public Function Insert_DOHeader(ByVal p_device_id As String, _
                                ByVal p_do_id As Integer, _
                                ByVal p_vehicle_no As String, _
                                ByVal p_requestor As String, _
                                ByVal p_location As String, _
                                ByVal p_trx_no As String, _
                                ByVal p_received_by_id As String, _
                                ByVal p_received_by As String, _
                                ByVal p_signature As String, _
                                ByVal p_signature_fn As String, _
                                ByVal p_signature_fs As Integer, _
                                ByVal p_created_date As String, _
                                ByVal p_real_rcv_by As String, _
                                ByVal p_real_rcv_by_nik As String) As String

        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd As OracleCommand
        Dim Result As String
        Dim ImageDataRaw As Byte()
        Dim ImageBlob As Byte()

        Dim ms As MemoryStream
        Dim imagex As Image
        Dim fs As FileStream
        Dim fsWS As FileStream
        Dim Fss As Int32


        ' ------ dump stream to file image to web server -------
        Try
            ImageDataRaw = Convert.FromBase64String(p_signature)

            ms = New MemoryStream(ImageDataRaw, 0, ImageDataRaw.Length)
            fs = New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/TransientStorage/") + p_signature_fn, FileMode.Create)
            ms.WriteTo(fs)
            ms.Close()
            fs.Close()
            fs.Dispose()

            fsWS = New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/TransientStorage/") + p_signature_fn, FileMode.Open, FileAccess.Read)
            Fss = System.Convert.ToInt32(fsWS.Length)
            ReDim ImageBlob(fsWS.Length)
            fsWS.Read(ImageBlob, 0, Fss)
            fsWS.Close()
        Catch ex As Exception
        End Try


        Dim xp_p_device_id As OracleParameter
        Dim xp_p_do_id As OracleParameter
        Dim xp_p_vehicle_no As OracleParameter
        Dim xp_p_requestor As OracleParameter
        Dim xp_p_location As OracleParameter
        Dim xp_p_trx_no As OracleParameter
        Dim xp_p_received_by_id As OracleParameter
        Dim xp_p_received_by As OracleParameter
        Dim xp_p_signature As OracleParameter
        Dim xp_p_signature_fn As OracleParameter
        Dim xp_p_signature_fs As OracleParameter
        Dim xp_p_created_date As OracleParameter
        Dim xp_p_do_header_id As OracleParameter
        Dim xp_p_real_rcv_by As OracleParameter
        Dim xp_p_real_rcv_by_nik As OracleParameter

        Result = ""
        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)


        Try
            OraDbcon.Open()
            cmd = New OracleCommand()

            '============== device id ==================
            xp_p_device_id = New OracleParameter
            With xp_p_device_id
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_device_id
                .ParameterName = "p_device_id"
            End With
            '============== do id ==================
            xp_p_do_id = New OracleParameter
            With xp_p_do_id
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Input
                .Value = p_do_id
                .ParameterName = "p_do_id"
            End With
            '============== vehicle no ==================
            xp_p_vehicle_no = New OracleParameter
            With xp_p_vehicle_no
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_vehicle_no
                .ParameterName = "p_vehicle_no"
            End With
            '============== requestor ==================
            xp_p_requestor = New OracleParameter
            With xp_p_requestor
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_requestor
                .ParameterName = "p_requestor"
            End With
            '============== location ==================
            xp_p_location = New OracleParameter
            With xp_p_location
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_location
                .ParameterName = "p_location"
            End With
            '============== trx no ==================
            xp_p_trx_no = New OracleParameter
            With xp_p_trx_no
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_trx_no
                .ParameterName = "p_trx_no"
            End With

            '============== received by id ==============
            xp_p_received_by_id = New OracleParameter
            With xp_p_received_by_id
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_received_by_id
                .ParameterName = "p_received_by_id"
            End With
            '============== received by ==================
            xp_p_received_by = New OracleParameter
            With xp_p_received_by
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_received_by
                .ParameterName = "p_received_by"
            End With

            '============== signature ==================
            xp_p_signature = New OracleParameter
            With xp_p_signature
                .OracleDbType = OracleDbType.Blob
                .Direction = ParameterDirection.Input
                .Size = Fss
                .Value = ImageBlob
                .ParameterName = "p_signature"
            End With
            '============== signature fn ==================
            xp_p_signature_fn = New OracleParameter
            With xp_p_signature_fn
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_signature_fn
                .ParameterName = "p_signature_fn"
            End With
            '============== signature fs ==================
            xp_p_signature_fs = New OracleParameter
            With xp_p_signature_fs
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Input
                .Value = Fss 'p_signature_fs
                .ParameterName = "p_signature_fs"
            End With
            '============== created date ==================
            xp_p_created_date = New OracleParameter
            With xp_p_created_date
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_created_date
                .ParameterName = "p_created_date"
            End With
            '============== do header id oracle ===========
            xp_p_do_header_id = New OracleParameter
            With xp_p_do_header_id
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Output
                .ParameterName = "p_do_header_id"
            End With
            '============== p_real_rcv_by ==================
            xp_p_real_rcv_by = New OracleParameter
            With xp_p_real_rcv_by
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_real_rcv_by
                .ParameterName = "p_real_rcv_by"
            End With
            '============== p_real_rcv_by_nik ==================
            xp_p_real_rcv_by_nik = New OracleParameter
            With xp_p_real_rcv_by_nik
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_real_rcv_by_nik
                .ParameterName = "p_real_rcv_by_nik"
            End With

            With cmd
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.INSERT_DO_HEADER"
                .Parameters.Add(xp_p_device_id)
                .Parameters.Add(xp_p_do_id)
                .Parameters.Add(xp_p_vehicle_no)
                .Parameters.Add(xp_p_requestor)
                .Parameters.Add(xp_p_location)
                .Parameters.Add(xp_p_trx_no)
                .Parameters.Add(xp_p_received_by_id)
                .Parameters.Add(xp_p_received_by)
                .Parameters.Add(xp_p_signature)
                .Parameters.Add(xp_p_signature_fn)
                .Parameters.Add(xp_p_signature_fs)
                .Parameters.Add(xp_p_created_date)
                .Parameters.Add(xp_p_real_rcv_by)
                .Parameters.Add(xp_p_real_rcv_by_nik)
                .Parameters.Add(xp_p_do_header_id)

                .BindByName = True
                .ExecuteNonQuery()

                Result = cmd.Parameters("p_do_header_id").Value.ToString

            End With
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try

        Return Result
    End Function

    <WebMethod()> _
    Public Function Insert_DODetail(ByVal p_device_id As String, _
                                    ByVal p_header_ora As Integer, _
                                    ByVal p_doh_id As Integer, _
                                    ByVal p_dod_id As Integer, _
                                    ByVal p_vehicle_no As String, _
                                    ByVal p_requestor As String, _
                                    ByVal p_location As String, _
                                    ByVal p_trx_no As String, _
                                    ByVal p_received_by_id As String, _
                                    ByVal p_received_by As String, _
                                    ByVal p_rak_persiapan As String, _
                                    ByVal p_bin_persiapan As String, _
                                    ByVal p_org_id As Integer, _
                                    ByVal p_item_id As Integer, _
                                    ByVal p_item_code As String, _
                                    ByVal p_item_desc As String, _
                                    ByVal p_quantity As Double, _
                                    ByVal p_quantity_rcv As Double, _
                                    ByVal p_uom As String, _
                                    ByVal p_reject_reason As String, _
                                    ByVal p_received_flag As String, _
                                    ByVal p_loading_date As String, _
                                    ByVal p_send_date As String, _
                                    ByVal p_created_date As String, _
                                    ByVal p_real_rcv_by As String, _
                                    ByVal p_real_rcv_by_nik As String, _
                                    ByVal p_loaded_by_id As String, _
                                    ByVal p_loaded_by As String) As String

        Dim OraDbcon As OracleConnection
        Dim OraConn As String
        Dim cmd As OracleCommand
        Dim Result As String

        'Dim p_received_flag As String
        Dim xp_p_device_id As OracleParameter
        Dim xp_p_header_ora As OracleParameter
        Dim xp_p_doh_id As OracleParameter
        Dim xp_p_dod_id As OracleParameter
        Dim xp_p_vehicle_no As OracleParameter
        Dim xp_p_requestor As OracleParameter
        Dim xp_p_location As OracleParameter
        Dim xp_p_trx_no As OracleParameter
        Dim xp_p_received_by_id As OracleParameter
        Dim xp_p_received_by As OracleParameter
        Dim xp_p_rak_persiapan As OracleParameter
        Dim xp_p_bin_persiapan As OracleParameter
        Dim xp_p_org_id As OracleParameter
        Dim xp_p_item_id As OracleParameter
        Dim xp_p_item_code As OracleParameter
        Dim xp_p_item_desc As OracleParameter
        Dim xp_p_quantity As OracleParameter
        Dim xp_p_quantity_rcv As OracleParameter
        Dim xp_p_uom As OracleParameter
        Dim xp_p_reject_reason As OracleParameter
        Dim xp_p_received_flag As OracleParameter
        Dim xp_p_loading_date As OracleParameter
        Dim xp_p_send_date As OracleParameter
        Dim xp_p_created_date As OracleParameter
        Dim xp_p_do_detail_id As OracleParameter
        Dim xp_p_real_rcv_by As OracleParameter
        Dim xp_p_real_rcv_by_nik As OracleParameter
        Dim xp_p_loaded_by_id As OracleParameter
        Dim xp_p_loaded_by As OracleParameter

        Result = ""
        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
        OraDbcon = New OracleConnection(OraConn)


        Try
            OraDbcon.Open()
            cmd = New OracleCommand()

            '============== device id ==================
            xp_p_device_id = New OracleParameter
            With xp_p_device_id
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_device_id
                .ParameterName = "p_device_id"
            End With
            '============== p_header_ora ==================
            xp_p_header_ora = New OracleParameter
            With xp_p_header_ora
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Input
                .Value = p_header_ora
                .ParameterName = "p_header_ora"
            End With
            '============== p_doh_id ==================
            xp_p_doh_id = New OracleParameter
            With xp_p_doh_id
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Input
                .Value = p_doh_id
                .ParameterName = "p_doh_id"
            End With
            '============== p_dod_id ==================
            xp_p_dod_id = New OracleParameter
            With xp_p_dod_id
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Input
                .Value = p_dod_id
                .ParameterName = "p_dod_id"
            End With
            '============== vehicle no ==================
            xp_p_vehicle_no = New OracleParameter
            With xp_p_vehicle_no
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_vehicle_no
                .ParameterName = "p_vehicle_no"
            End With
            '============== requestor ==================
            xp_p_requestor = New OracleParameter
            With xp_p_requestor
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_requestor
                .ParameterName = "p_requestor"
            End With
            '============== location ==================
            xp_p_location = New OracleParameter
            With xp_p_location
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_location
                .ParameterName = "p_location"
            End With
            '============== p_trx no ==================
            xp_p_trx_no = New OracleParameter
            With xp_p_trx_no
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_trx_no
                .ParameterName = "p_trx_no"
            End With
            '============== p_received by id ==================
            xp_p_received_by_id = New OracleParameter
            With xp_p_received_by_id
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_received_by_id
                .ParameterName = "p_received_by_id"
            End With
            '============== p_received by ==================
            xp_p_received_by = New OracleParameter
            With xp_p_received_by
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_received_by
                .ParameterName = "p_received_by"
            End With
            '============== p_rak_persiapan ==================
            xp_p_rak_persiapan = New OracleParameter
            With xp_p_rak_persiapan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_rak_persiapan
                .ParameterName = "p_rak_persiapan"
            End With
            '============== p_bin_perisapan ==================
            xp_p_bin_persiapan = New OracleParameter
            With xp_p_bin_persiapan
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_bin_persiapan
                .ParameterName = "p_bin_persiapan"
            End With
            '============== p_org_id ==================
            xp_p_org_id = New OracleParameter
            With xp_p_org_id
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Input
                .Value = p_org_id
                .ParameterName = "p_org_id"
            End With
            '============== p_item_id ==================
            xp_p_item_id = New OracleParameter
            With xp_p_item_id
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Input
                .Value = p_item_id
                .ParameterName = "p_item_id"
            End With
            '============== p_item_code ==================
            xp_p_item_code = New OracleParameter
            With xp_p_item_code
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_item_code
                .ParameterName = "p_item_code"
            End With
            '============== p_item_desc ================
            xp_p_item_desc = New OracleParameter
            With xp_p_item_desc
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_item_desc
                .ParameterName = "p_item_desc"
            End With
            '============== p_quantity ==================
            xp_p_quantity = New OracleParameter
            With xp_p_quantity
                .OracleDbType = OracleDbType.Single
                .Direction = ParameterDirection.Input
                .Value = p_quantity
                .ParameterName = "p_quantity"
            End With
            '============== p_quantity_rcv ==================
            xp_p_quantity_rcv = New OracleParameter
            With xp_p_quantity_rcv
                .OracleDbType = OracleDbType.Single
                .Direction = ParameterDirection.Input
                .Value = p_quantity_rcv
                .ParameterName = "p_quantity_rcv"
            End With
            '============== p_uom ================
            xp_p_uom = New OracleParameter
            With xp_p_uom
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_uom
                .ParameterName = "p_uom"
            End With
            '============== p_reject_reason ================
            xp_p_reject_reason = New OracleParameter
            With xp_p_reject_reason
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_reject_reason
                .ParameterName = "p_reject_reason"
            End With

            '============== p_received_flag ================
            'If (Trim(p_reject_reason).ToString.Length > 0) Then
            'p_received_flag = "N"
            'Else
            'p_received_flag = "Y"
            'End If

            xp_p_received_flag = New OracleParameter
            With xp_p_received_flag
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_received_flag
                .ParameterName = "p_received_flag"
            End With

            '============== p_loading_date ==================
            xp_p_loading_date = New OracleParameter
            With xp_p_loading_date
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_loading_date
                .ParameterName = "p_loading_date"
            End With
            '============== p_send_date ==================
            xp_p_send_date = New OracleParameter
            With xp_p_send_date
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_send_date
                .ParameterName = "p_send_date"
            End With
            '============== p_created date ==================
            xp_p_created_date = New OracleParameter
            With xp_p_created_date
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_created_date
                .ParameterName = "p_created_date"
            End With
            '============== p_do_detail_id oracle ===========
            xp_p_do_detail_id = New OracleParameter
            With xp_p_do_detail_id
                .OracleDbType = OracleDbType.Int64
                .Direction = ParameterDirection.Output
                .ParameterName = "p_do_detail_id"
            End With

            '============== p_real_rcv_by ==================
            xp_p_real_rcv_by = New OracleParameter
            With xp_p_real_rcv_by
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_real_rcv_by
                .ParameterName = "p_real_rcv_by"
            End With
            '============== p_real_rcv_by_nik ==================
            xp_p_real_rcv_by_nik = New OracleParameter
            With xp_p_real_rcv_by_nik
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_real_rcv_by_nik
                .ParameterName = "p_real_rcv_by_nik"
            End With

            '============== p_loaded by id ==================
            xp_p_loaded_by_id = New OracleParameter
            With xp_p_loaded_by_id
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_loaded_by_id
                .ParameterName = "p_loaded_by_id"
            End With
            '============== p_loaded by ==================
            xp_p_loaded_by = New OracleParameter
            With xp_p_loaded_by
                .OracleDbType = OracleDbType.Varchar2
                .Direction = ParameterDirection.Input
                .Value = p_loaded_by
                .ParameterName = "p_loaded_by"
            End With



            With cmd
                .Connection = OraDbcon
                .CommandType = CommandType.StoredProcedure
                .CommandText = "APPS.GGGG_DOMAE_PKG.INSERT_DO_DETAIL"
                .Parameters.Add(xp_p_device_id)
                .Parameters.Add(xp_p_header_ora)
                .Parameters.Add(xp_p_doh_id)
                .Parameters.Add(xp_p_dod_id)
                .Parameters.Add(xp_p_vehicle_no)
                .Parameters.Add(xp_p_requestor)
                .Parameters.Add(xp_p_location)
                .Parameters.Add(xp_p_trx_no)
                .Parameters.Add(xp_p_received_by_id)
                .Parameters.Add(xp_p_received_by)
                .Parameters.Add(xp_p_rak_persiapan)
                .Parameters.Add(xp_p_bin_persiapan)
                .Parameters.Add(xp_p_org_id)
                .Parameters.Add(xp_p_item_id)
                .Parameters.Add(xp_p_item_code)
                .Parameters.Add(xp_p_item_desc)
                .Parameters.Add(xp_p_quantity)
                .Parameters.Add(xp_p_quantity_rcv)
                .Parameters.Add(xp_p_uom)
                .Parameters.Add(xp_p_reject_reason)
                .Parameters.Add(xp_p_received_flag)
                .Parameters.Add(xp_p_loading_date)
                .Parameters.Add(xp_p_send_date)
                .Parameters.Add(xp_p_created_date)
                .Parameters.Add(xp_p_real_rcv_by)
                .Parameters.Add(xp_p_real_rcv_by_nik)
                .Parameters.Add(xp_p_loaded_by_id)
                .Parameters.Add(xp_p_loaded_by)
                .Parameters.Add(xp_p_do_detail_id)

                .BindByName = True
                .ExecuteNonQuery()

                Result = cmd.Parameters("p_do_detail_id").Value.ToString


            End With
        Catch ex As Exception
        Finally
            OraDbcon.Close()
            OraDbcon.Dispose()
        End Try
        Return Result
    End Function

    '========= obsolete replaced by Get_List_No_Trx v.1.4
    '    <WebMethod()> _
    'Public Function Get_List_BinPersiapan(ByVal p_rak_persiapan As String) As DataTable
    ' Dim OraDbcon As OracleConnection
    ' Dim OraConn As String
    ' Dim cmd_ps As OracleCommand
    ' Dim da_ps As OracleDataAdapter
    ' Dim ds_ps As DataSet
    '
    '    Dim XResult_lo As OracleParameter
    '    Dim xRakPersiapan As OracleParameter
    '
    '        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
    '        OraDbcon = New OracleConnection(OraConn)
    '        ds_ps = New DataSet
    '
    '        Try
    '            OraDbcon.Open()
    '            cmd_ps = New OracleCommand()
    '
    '            xRakPersiapan = New OracleParameter
    '            With xRakPersiapan
    '                .OracleDbType = OracleDbType.Varchar2
    '                .Direction = ParameterDirection.Input
    '                .Value = p_rak_persiapan
    '                .ParameterName = "p_rak_persiapan"
    '            End With
    '
    '            XResult_lo = New OracleParameter
    '            With XResult_lo
    '                .OracleDbType = OracleDbType.RefCursor
    '                .Direction = ParameterDirection.Output
    '                .ParameterName = "p_recordset"
    '            End With
    '
    '            With cmd_ps
    '                .Connection = OraDbcon
    '                .CommandType = CommandType.StoredProcedure
    '                .CommandText = "APPS.GGGG_DOMAE_PKG.GET_LIST_BIN_PERSIAPAN"
    '                .Parameters.Add(XResult_lo)
    '                .Parameters.Add(xRakPersiapan)
    '                .BindByName = True
    '                .ExecuteNonQuery()
    '            End With
    '
    '            Try
    '                da_ps = New OracleDataAdapter(cmd_ps)
    '                da_ps.Fill(ds_ps)
    '            Catch ex As Exception
    '            End Try
    '        Catch ex As Exception
    '        Finally
    '            OraDbcon.Close()
    '            OraDbcon.Dispose()
    '        End Try
    '
    '        Return ds_ps.Tables(0)
    '
    '    End Function


    ' ========== obsolete replaced by Get_List_Manifest_By_Trx v.1.4
    '    <WebMethod()> _
    'Public Function Get_List_Manifest(ByVal p_no_kendaraan As String, _
    '                                  ByVal p_rak_persiapan As String, _
    '                                  ByVal p_bin_persiapan As String) As DataTable
    'Dim OraDbcon As OracleConnection
    'Dim OraConn As String
    'Dim cmd_lo As OracleCommand
    'Dim da_lo As OracleDataAdapter
    'Dim ds_lo As DataSet
    'Dim dt_lo As DataTable
    '
    '    Dim XResult_lo As OracleParameter
    '    Dim XNoKendaraan As OracleParameter
    '    Dim XRakPersiapan As OracleParameter
    '    Dim XBinPersiapan As OracleParameter
    '
    '        OraConn = ConfigurationManager.ConnectionStrings("OraConn").ConnectionString
    '        OraDbcon = New OracleConnection(OraConn)
    '
    '        dt_lo = New DataTable
    '        ds_lo = New DataSet
    '
    '        Try
    '            OraDbcon.Open()
    '            cmd_lo = New OracleCommand()
    '
    '            XNoKendaraan = New OracleParameter
    '            With XNoKendaraan
    '                .OracleDbType = OracleDbType.Varchar2
    '                .Direction = ParameterDirection.Input
    '                .Value = p_no_kendaraan
    '                .ParameterName = "p_no_kendaraan"
    '            End With
    '
    '            XRakPersiapan = New OracleParameter
    '            With XRakPersiapan
    '                .OracleDbType = OracleDbType.Varchar2
    '                .Direction = ParameterDirection.Input
    '                .Value = p_rak_persiapan
    '                .ParameterName = "p_rak_persiapan"
    '            End With
    '
    '            XBinPersiapan = New OracleParameter
    '            With XBinPersiapan
    '                .OracleDbType = OracleDbType.Varchar2
    '                .Direction = ParameterDirection.Input
    '                .Value = p_bin_persiapan
    '                .ParameterName = "p_bin_persiapan"
    '            End With
    '
    '            XResult_lo = New OracleParameter
    '            With XResult_lo
    '                .OracleDbType = OracleDbType.RefCursor
    '                .Direction = ParameterDirection.Output
    '                .ParameterName = "p_recordset"
    '            End With
    '
    '            With cmd_lo
    '                .Connection = OraDbcon
    '                .CommandType = CommandType.StoredProcedure
    '                .CommandText = "APPS.GGGG_DOMAE_PKG.GET_LIST_MANIFEST"
    '                .Parameters.Add(XResult_lo)
    '                .Parameters.Add(XNoKendaraan)
    '                .Parameters.Add(XRakPersiapan)
    '                .Parameters.Add(XBinPersiapan)
    '                .BindByName = True
    '                .ExecuteNonQuery()
    '            End With
    '
    '            Try
    '                da_lo = New OracleDataAdapter(cmd_lo)
    '                da_lo.Fill(ds_lo)
    '            Catch ex As Exception
    '            End Try
    '        Catch ex As Exception
    '        Finally
    '            OraDbcon.Close()
    '            OraDbcon.Dispose()
    '        End Try
    '
    '       If (ds_lo.Tables(0) Is Nothing) Then
    '
    '        End If
    '
    '        dt_lo = ds_lo.Tables(0)
    '
    '        Return dt_lo
    '
    '    End Function


End Class