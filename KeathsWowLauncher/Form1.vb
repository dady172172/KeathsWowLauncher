Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Public Class Form1
    'Discord link..
    Dim DiscordLink As String = "https://www.google.com"
    'Website link..
    Dim WebsiteLink As String = "https://www.google.com"
    'AnnouncementsLink is the Raw text for announcements
    Shared AnnouncementsLink As String = "https://pastebin.com/raw/R5faAcDw"
    'AddonsXMLLIstLink is Downloads the file to the current directory. This file is the is the list of Addons for download.
    Shared AddonsXMLListLink As String = "https://raw.githubusercontent.com/dady172172/KeathsWowLauncher/master/KeathsWowLauncher/AddonsList.xml"
    'array of addons
    Dim addonsArray As New ArrayList()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'round the corners of the Form
        Settings.roundCorners(Me)
        'shows dashboard panel
        Loadup.Panel(pnlDashboard)
        'load settings
        Settings.Load()
        'load announcements and addons
        Loadup.Announcement(AnnouncementsLink)
        Loadup.Addons_Current_list(AddonsXMLListLink)
        Loadup.Addons_Installable_list(AddonsXMLListLink)
        'set addonsArray so we can use it for addon infomation
        addonsArray = Loadup.Addon_XML_To_Array()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        'closes the program
        Me.Close()
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        'again does what it says
        Loadup.Panel(pnlDashboard)
    End Sub

    Private Sub btnDelConfig_Click(sender As Object, e As EventArgs) Handles btnDelConfig.Click
        'if config exist delete it
        If File.Exists(txtWowDir.Text & "\WTF\Config.wtf") Then
            File.Delete(txtWowDir.Text & "\WTF\Config.wtf")
        Else
            MessageBox.Show("Can not find config file! Please make sure you have selected your Wow folder on the Dashboard.")
        End If
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        'does what it says
        Loadup.Panel(pnlSettings)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnAddRealmslist.Click
        'variables
        Dim rldt As String = txtRealmslistDashboard.Text
        'check if dropdown textbox does not contains words then we add the dropdown text to the dropdown list
        If rldt IsNot Nothing And rldt IsNot "" Then
            txtRealmslistDashboard.Items.Add(rldt)
        End If
    End Sub

    Private Sub btnDeleteRealmslist_Click(sender As Object, e As EventArgs) Handles btnDeleteRealmslist.Click
        'variables
        Dim rldt As String = txtRealmslistDashboard.Text
        'check if dropdown cantains selected text then delete it
        If txtRealmslistDashboard.Items.Contains(rldt) Then
            txtRealmslistDashboard.Items.Remove(rldt)
        End If
        'clear the dropdown textbox
        txtRealmslistDashboard.Text = ""
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'save settings
        Settings.Save()
    End Sub

    Private Sub btnSearchDir_click(sender As Object, e As EventArgs) Handles btnSearchDir.Click
        'Create folder browser dialog
        Dim fbd As New FolderBrowserDialog
        'Set discription for FolderBrowserDialog
        fbd.Description = "Please Select you Wow folder where your Wow.exe is."
        'Check result(that user selected a path) and set the Textbox text to the users selection
        If fbd.ShowDialog() = DialogResult.OK Then
            txtWowDir.Text = fbd.SelectedPath()
        End If
    End Sub

    Private Sub btnDiscord_Click(sender As Object, e As EventArgs) Handles btnDiscord.Click
        'Opens discord link in browser
        Process.Start(DiscordLink)
    End Sub

    Private Sub btnWebsite_Click(sender As Object, e As EventArgs) Handles btnWebsite.Click
        'Opens website link in browser
        Process.Start(WebsiteLink)
    End Sub

    Private Sub btnPlay_Click(sender As Object, e As EventArgs) Handles btnPlay.Click
        'checks
        If txtRealmslistDashboard.SelectedItem = "" Then
            MessageBox.Show("Please select a realm!")
            Exit Sub
        End If
        If Not File.Exists(txtWowDir.Text & "\Data\enUS\realmlist.wtf") Then
            MessageBox.Show("Please select a realm or add one!")
            Exit Sub
        End If
        If txtWowDir.Text = "" Then
            MessageBox.Show("Please select the main directory where your Wow.exe is.")
            Exit Sub
        End If
        If Not File.Exists(txtWowDir.Text & "\Wow.exe") Then
            MessageBox.Show("Could not Find Wow.exe")
            Exit Sub
        End If
        'write realmlist.wtf
        File.Delete(txtWowDir.Text & "\Data\enUS\realmlist.wtf")
        File.WriteAllText(txtWowDir.Text & "\Data\enUS\realmlist.wtf", "set realmlist " & txtRealmslistDashboard.SelectedItem)
        'get args if any selected in settings
        Dim tmp As New ArrayList()
        If cbConsole.Checked Then tmp.Add("-console")
        If cbfullscreen.Checked Then tmp.Add("-fullscreen")
        If cbMaxscreen.Checked Then tmp.Add("-fullscreen")
        If cbNosound.Checked Then tmp.Add("-nosound")
        If cbWindowed.Checked Then tmp.Add("-windowed")
        Dim args As String
        For Each item In tmp
            If args = "" Then
                args = item & " "
            Else
                args = args & item & " "
            End If
        Next
        'start wow.exe
        Process.Start(txtWowDir.Text & "\Wow.exe", args)
    End Sub

    Private Sub btnDelCache_Click(sender As Object, e As EventArgs) Handles btnDelCache.Click
        'check if directory exists then delete cache folder
        If Directory.Exists(txtWowDir.Text & "\Cache") Then
            Directory.Delete(txtWowDir.Text & "\Cache", True)
        Else
            MessageBox.Show("Can not find cache folder! Please make sure you have selected your Wow folder on the Dashboard.")
        End If
    End Sub

    Private Sub btnResetConfig_Click(sender As Object, e As EventArgs) Handles btnResetConfig.Click
        'check if config exists then delete and create/write all lines to config
        If File.Exists(txtWowDir.Text & "\WTF\Config.wtf") Then
            File.Delete(txtWowDir.Text & "\WTF\Config.wtf")
            Dim tmp() As String = {"SET locale ""enUS""", "SET realmList ""keaths-network.com""", "SET hwDetect ""0""", "SET gxWindow ""1""", "SET gxResolution ""800x600""", "SET gxRefresh ""60""", "SET gxMultisampleQuality ""0.000000""", "SET gxFixLag ""0""", "SET videoOptionsVersion ""3""", "SET textureFilteringMode ""0""", "SET movie ""0""", "SET Gamma ""1.000000""", "SET readTOS ""1""", "SET readEULA ""1""", "SET showToolsUI ""1""", "SET Sound_OutputDriverName ""System Default""", "SET Sound_MusicVolume ""0.40000000596046""", "SET Sound_AmbienceVolume ""0.60000002384186""", "SET farclip ""177""", "SET particleDensity ""0.10000000149012""", "SET baseMip ""1""", "SET environmentDetail ""0.5""", "SET weatherDensity ""0""", "SET ffxGlow ""0""", "SET ffxDeath ""0"""}
            File.WriteAllLines(txtWowDir.Text & "\WTF\Config.wtf", tmp)
        Else
            MessageBox.Show("Can not find config file! Please make sure you have selected your Wow folder on the Dashboard.")
        End If
    End Sub

    'Draggable panel variables
    Dim draggable As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer
    Private Sub pnlTop_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseDown
        'draggable panel
        draggable = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub
    Private Sub pnlTop_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseMove
        'draggable panel
        If draggable Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub
    Private Sub pnlTop_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseUp
        'draggable panel
        draggable = False
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        'minimize window
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnAddons_Click(sender As Object, e As EventArgs) Handles btnAddons.Click
        'show addons panel
        Loadup.Addons_Current_list(AddonsXMLListLink)
        Loadup.Addons_Installable_list(AddonsXMLListLink)
        Loadup.Panel(pnlAddons)
    End Sub

    Private Sub btnInstallAddon_Click(sender As Object, e As EventArgs) Handles btnInstallAddon.Click
        'set variable of selected item in lisbox(install addon)
        Dim selname As String = lbInstallAddons.SelectedItem.ToString
        'check if a item is selected
        If selname = Nothing Then Exit Sub
        'check if addonsArray(xml addons info) has data 
        If addonsArray(0) Is Nothing Then Exit Sub
        'varibles
        Dim dlLink As String = Nothing
        Dim dlName As String = Nothing
        'loop through addonsArray(xml addons info) and get download link 
        For i = 0 To addonsArray.Count - 1
            If addonsArray(i)(0).ToString = selname Then
                dlLink = addonsArray(i)(1).ToString
                dlName = addonsArray(i)(0).ToString
                Exit For
            End If
        Next
        '' temp download.. I plan on adding multiselection and a Form
        If dlLink Is Nothing Or dlName Is Nothing Then Exit Sub
        If Directory.Exists("Downloads") Then Directory.Delete("Downloads", True)
        If Not Directory.Exists("Downloads") Then Directory.CreateDirectory("Downloads")
        Dim dl As New WebClient
        Try
            Dim bites = dl.DownloadData(New Uri(dlLink))
            File.WriteAllBytes("Downloads\" & dlName & ".zip", bites)
        Catch ex As Exception
            MessageBox.Show("Could not download the file!" & vbNewLine & ex.Message)
            Exit Sub
        End Try
        'unzip into addons directory
        Try
            ZipFile.ExtractToDirectory("Downloads\" & dlName & ".zip", txtWowDir.Text & "\Interface\AddOns")
        Catch ex As Exception
            MessageBox.Show("You may already have this addon installed." & vbNewLine & ex.Message)
            Directory.Delete("Downloads", True)
            Exit Sub
        End Try
        'make sure to clean up temp folder
        Directory.Delete("Downloads", True)
        'tell user finished download/unzip
        MessageBox.Show("AddonName: " & dlName & vbNewLine & "Done downloading and extracting zip. Reloading current addons list.")
        'reload current addons list so it shows the new addon in the listbox
        Loadup.Addons_Current_list(AddonsXMLListLink)
    End Sub

    Private Sub txtAddonInstallSearch_TextChanged(sender As Object, e As EventArgs) Handles txtAddonInstallSearch.TextChanged
        'varibles
        Dim txt As String = txtAddonInstallSearch.Text.ToLower
        Dim tmplist As New ArrayList()
        'build tmplist to hold names of adddons
        If txt = Nothing Then
            lbInstallAddons.Items.Clear()
            For i = 0 To addonsArray.Count - 1
                tmplist.Add(addonsArray(i)(0))
            Next
            tmplist.Sort()
            lbInstallAddons.Items.AddRange(tmplist.ToArray)
            Exit Sub
        End If
        'check if addon name contains text from searchbox
        For i = 0 To addonsArray.Count - 1
            If addonsArray(i)(0).ToString.ToLower.Contains(txt) Then
                tmplist.Add(addonsArray(i)(0))
            End If
        Next
        'repopulate listbox with matched addon names
        If tmplist IsNot Nothing Then
            lbInstallAddons.Items.Clear()
            lbInstallAddons.Items.AddRange(tmplist.ToArray)
        End If

    End Sub
End Class
