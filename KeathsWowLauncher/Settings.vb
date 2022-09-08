Public Class Settings
    'load settings
    Shared Sub Load()
        If My.Settings.wowdir IsNot "" Then Form1.txtWowDir.Text = My.Settings.wowdir
        If My.Settings.realmslistselected IsNot "" Then Form1.txtRealmslistDashboard.Text = My.Settings.realmslistselected
        Form1.cbConsole.Checked = My.Settings.cbConsole
        Form1.cbfullscreen.Checked = My.Settings.cbFullscreen
        Form1.cbMaxscreen.Checked = My.Settings.cbMaxscreen
        Form1.cbNosound.Checked = My.Settings.cbNosound
        Form1.cbWindowed.Checked = My.Settings.cbWindowed
        If My.Settings.realmslist IsNot "" Then
            Dim tempArray() = My.Settings.realmslist.Split(";")
            Form1.txtRealmslistDashboard.Items.AddRange(tempArray)
        End If
        If Form1.txtRealmslistDashboard.Text IsNot "" And Form1.txtRealmslistDashboard.Items.Contains(Form1.txtRealmslistDashboard.Text) Then Form1.txtRealmslistDashboard.SelectedItem = Form1.txtRealmslistDashboard.Text
    End Sub
    'save settings
    Shared Sub Save()
        If Not Form1.txtWowDir.Text = "" Then My.Settings.wowdir = Form1.txtWowDir.Text
        If Not Form1.txtRealmslistDashboard.SelectedItem = "" Then My.Settings.realmslistselected = Form1.txtRealmslistDashboard.SelectedItem
        If Form1.cbConsole.Checked Then My.Settings.cbConsole = True Else My.Settings.cbConsole = False
        If Form1.cbfullscreen.Checked Then My.Settings.cbFullscreen = True Else My.Settings.cbFullscreen = False
        If Form1.cbMaxscreen.Checked Then My.Settings.cbMaxscreen = True Else My.Settings.cbMaxscreen = False
        If Form1.cbNosound.Checked Then My.Settings.cbNosound = True Else My.Settings.cbNosound = False
        If Form1.cbWindowed.Checked Then My.Settings.cbWindowed = True Else My.Settings.cbWindowed = False
        Dim tmpstring As String = ""
        If Form1.txtRealmslistDashboard.Items.Count > 0 Then
            For i = 0 To Form1.txtRealmslistDashboard.Items.Count - 1
                Select Case i
                    Case 0
                        tmpstring = Form1.txtRealmslistDashboard.Items.Item(i) & ";"
                    Case (Form1.txtRealmslistDashboard.Items.Count - 1)
                        tmpstring &= Form1.txtRealmslistDashboard.Items.Item(i)
                    Case Else
                        tmpstring = tmpstring & Form1.txtRealmslistDashboard.Items.Item(i) & ";"
                End Select
            Next
            My.Settings.realmslist = tmpstring
        End If
        My.Settings.Save()

    End Sub
    'creates rounded corners on form
    Shared Sub RoundCorners(obj As Form)
        Dim DGP As New Drawing2D.GraphicsPath
        DGP.StartFigure()
        'top left corner
        DGP.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        DGP.AddLine(40, 0, obj.Width - 40, 0)

        'top right corner
        DGP.AddArc(New Rectangle(obj.Width - 40, 0, 40, 40), -90, 90)
        DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40)

        'buttom right corner
        DGP.AddArc(New Rectangle(obj.Width - 40, obj.Height - 40, 40, 40), 0, 90)
        DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height)

        'buttom left corner
        DGP.AddArc(New Rectangle(0, obj.Height - 40, 40, 40), 90, 90)
        DGP.CloseFigure()

        obj.Region = New Region(DGP)
    End Sub
End Class
