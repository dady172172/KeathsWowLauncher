Imports System.IO
Imports System.Net
Imports System.Xml
Public Class Loadup
    'Loads addons folder name to listbox
    Shared Sub Addons_Current_list(ByVal AddonListDownloadLink As String)
        If Not Directory.Exists(Form1.txtWowDir.Text & "\Interface\AddOns") Then
            Form1.listboxInstalledAddons.Items.Add("Could not find ""AddOns"" folder")
            Exit Sub
        End If
        Form1.listboxInstalledAddons.Items.Clear()
        For Each item In Directory.GetDirectories(Form1.txtWowDir.Text & "\Interface\AddOns")
            If File.Exists(item & "\" & Path.GetFileName(item) & ".toc") And Not item.Contains("Blizzard_") Then
                Form1.listboxInstalledAddons.Items.Add(Path.GetFileName(item))
            End If
        Next
    End Sub
    'loads xml addons(that can be installed) in to listbox
    Shared Sub Addons_Installable_list(ByVal AddonListDownloadLink As String)
        If Not File.Exists("AddonsList.xml") Then
            Dim client As New WebClient
            Try
                client.DownloadFile(AddonListDownloadLink, "AddonsList.xml")
            Catch ex As Exception
                Form1.lbInstallAddons.Items.Clear()
                Form1.lbInstallAddons.Items.Add("Could not download list of addons!" & vbNewLine & ex.Message)
                Exit Sub
            End Try
        End If
        Form1.lbInstallAddons.Items.Clear()
        Dim tmpArray As New ArrayList()
        tmpArray.AddRange(Addon_XML_To_Array())
        Dim tmpname As New ArrayList()
        For i = 0 To tmpArray.Count - 1
            If tmpArray(0) IsNot Nothing Then
                tmpname.Add(tmpArray(i)(0).ToString)
            End If
        Next
        tmpname.ToArray()
        tmpname.Sort()
        Form1.lbInstallAddons.Items.AddRange(tmpname.ToArray)
    End Sub
    'gets data from addons xml and returns an array
    Shared Function Addon_XML_To_Array()
        If Not File.Exists("AddonsList.xml") Then Return Nothing
        Dim tmplist As New ArrayList()
        Dim xmld As New XmlDocument
        xmld.Load("AddonsList.xml")
        Dim xmlnl As XmlNodeList = xmld.GetElementsByTagName("addon")
        For Each xmln As XmlNode In xmlnl
            If xmln.Item("name").InnerText IsNot Nothing Then
                tmplist.Add({xmln.Item("name").InnerText, xmln.Item("downloadname").InnerText, xmln.Item("tooltip").InnerText})
            End If
        Next
        Return tmplist
    End Function
    'Loads announcements
    Shared Sub Announcement(ByVal AnnouncementsLink As String)
        Dim client As New WebClient
        Dim tmptxt As String
        Try
            tmptxt = client.DownloadString(AnnouncementsLink)
        Catch ex As Exception
            MessageBox.Show("Can not download announcements string from provided address!" & vbNewLine & ex.Message.ToString)
            tmptxt = "Can not download announcements string from provided address!" & vbNewLine & ex.Message.ToString
        End Try
        Form1.txtUpdateInfo.Text = tmptxt
    End Sub
    'shows/hides selected panel
    Shared Sub Panel(ByVal Pnl As Panel)
        'Hide all the panels
        Form1.pnlSettings.Hide()
        Form1.pnlDashboard.Hide()
        Form1.pnlAddons.Hide()
        'check what panel should be shown and show it
        Select Case Pnl.Name
            Case Form1.pnlSettings.Name
                Form1.pnlSettings.Show()
                Form1.lblNav.Text = "Settings"
            Case Form1.pnlDashboard.Name
                Form1.pnlDashboard.Show()
                Form1.lblNav.Text = "Dashboard"
            Case Form1.pnlAddons.Name
                Form1.pnlAddons.Show()
                Form1.lblNav.Text = "Addons"
            Case Else
                Form1.Close()
        End Select
    End Sub
End Class
