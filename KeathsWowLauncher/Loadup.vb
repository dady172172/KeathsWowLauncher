Imports System.IO
Imports System.Net
Imports System.Xml
Public Class Loadup


    Shared Sub Addons(ByVal AddonListDownloadLink As String)
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

        '-----------------------------------------------------------------
        If File.Exists("AddonsList.xml") Then File.Delete("AddonsList.xml")
        Dim client As New WebClient
        Try
            client.DownloadFile(AddonListDownloadLink, "AddonsList.xml")
        Catch ex As Exception
            Form1.lbInstallAddons.Items.Clear()
            Form1.lbInstallAddons.Items.Add("Could not download list of addons!" & vbNewLine & ex.Message)
            Exit Sub
        End Try
        Form1.lbInstallAddons.Items.Clear()
        Dim tmplist As New ArrayList()
        Dim xmld As New XmlDocument
        xmld.Load("AddonsList.xml")
        Dim xmlnl As XmlNodeList = xmld.GetElementsByTagName("addon")
        For Each xmln As XmlNode In xmlnl
            If xmln.Item("name").InnerText IsNot Nothing Then
                tmplist.Add(xmln.Item("name").InnerText)
            End If
        Next
        tmplist.Sort()
        For i = 0 To tmplist.Count - 1
            Form1.lbInstallAddons.Items.Add(tmplist(i))
        Next
    End Sub

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
