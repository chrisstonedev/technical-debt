VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   7350
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   9735
   LinkTopic       =   "Form1"
   ScaleHeight     =   7350
   ScaleWidth      =   9735
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox postOutputJsonTextBox 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   4800
      MultiLine       =   -1  'True
      TabIndex        =   14
      Top             =   4440
      Width           =   4095
   End
   Begin VB.TextBox postTranslateJsonTextBox 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   13
      Top             =   4440
      Width           =   4335
   End
   Begin VB.CommandButton getAltButton 
      Caption         =   "GET alt"
      Height          =   735
      Left            =   7560
      TabIndex        =   8
      Top             =   720
      Width           =   1095
   End
   Begin VB.TextBox getOutputXmlTextBox 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   4800
      MultiLine       =   -1  'True
      TabIndex        =   6
      Top             =   2520
      Width           =   4095
   End
   Begin VB.TextBox statusCodeTextBox 
      Enabled         =   0   'False
      Height          =   285
      Left            =   7200
      TabIndex        =   5
      Top             =   90
      Width           =   1335
   End
   Begin VB.TextBox getOutputJsonTextBox 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   4
      Top             =   2520
      Width           =   4335
   End
   Begin VB.CommandButton postButton 
      Caption         =   "POST"
      Height          =   495
      Left            =   5880
      TabIndex        =   3
      Top             =   1560
      Width           =   2775
   End
   Begin VB.CommandButton getMainButton 
      Caption         =   "GET main"
      Height          =   735
      Left            =   5880
      TabIndex        =   2
      Top             =   720
      Width           =   1455
   End
   Begin VB.Frame userIdFrame 
      Caption         =   "User ID"
      Height          =   495
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   2535
      Begin VB.TextBox userIdTextBox 
         Appearance      =   0  'Flat
         BackColor       =   &H8000000F&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Fixedsys"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C000C0&
         Height          =   285
         Left            =   60
         TabIndex        =   7
         Text            =   "First Name"
         Top             =   180
         Width           =   2415
      End
   End
   Begin VB.Frame methodFrame 
      Caption         =   "Method"
      Height          =   495
      Left            =   2700
      TabIndex        =   1
      Top             =   0
      Width           =   1215
      Begin VB.TextBox methodTextBox 
         Appearance      =   0  'Flat
         BackColor       =   &H8000000F&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Fixedsys"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C000C0&
         Height          =   285
         Left            =   60
         TabIndex        =   16
         Text            =   "0"
         Top             =   180
         Width           =   900
      End
   End
   Begin VB.Label statusCodeLabel 
      Caption         =   "Status Code:"
      Height          =   375
      Left            =   5880
      TabIndex        =   15
      Top             =   120
      Width           =   3015
   End
   Begin VB.Label Label4 
      Caption         =   "POST Output in JSON"
      Height          =   375
      Left            =   4800
      TabIndex        =   12
      Top             =   4200
      Width           =   3015
   End
   Begin VB.Label Label3 
      Caption         =   "GET Output in XML"
      Height          =   375
      Left            =   4800
      TabIndex        =   11
      Top             =   2280
      Width           =   3015
   End
   Begin VB.Label Label2 
      Caption         =   "POST Translate in JSON"
      Height          =   375
      Left            =   120
      TabIndex        =   10
      Top             =   4200
      Width           =   3015
   End
   Begin VB.Label Label1 
      Caption         =   "GET Output in JSON"
      Height          =   375
      Left            =   120
      TabIndex        =   9
      Top             =   2280
      Width           =   3015
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private OurNewMethods As ClassLibrary.NewMethods

Private Sub Form_Initialize()
    Set OurNewMethods = New ClassLibrary.NewMethods
End Sub

Private Sub getMainButton_Click()
    SendGetRequest
End Sub

Private Sub getAltButton_Click()
    Call SendGetRequest(userIdTextBox.Text)
End Sub

Private Sub postButton_Click()
    Dim httpRequest As WinHttp.WinHttpRequest
    Set httpRequest = New WinHttp.WinHttpRequest
    Dim url As String
    url = "https://jsonplaceholder.typicode.com/posts"
    httpRequest.Open "POST", url, False
    httpRequest.SetRequestHeader "Content-type", "application/json"

    Dim xmlDocument As DOMDocument60
    Dim successfulLoad As Boolean
    Set xmlDocument = New DOMDocument60
    successfulLoad = xmlDocument.loadXML(getOutputXmlTextBox.Text)
    If successfulLoad Then
        Dim node As IXMLDOMNode
        Dim nodeList As IXMLDOMNodeList
        Set nodeList = xmlDocument.selectNodes("/xml/*")
        Dim jsonText As String
        For Each node In nodeList
            If Len(jsonText) = 0 Then
                jsonText = "{""" + node.baseName + """: """ + node.Text + """"
            Else
                jsonText = jsonText + ",""" + node.baseName + """: """ + node.Text + """"
            End If
        Next node
        If Len(jsonText) > 0 Then
            jsonText = jsonText + "}"
        End If
    Else
        getOutputXmlTextBox.Text = xmlDocument.parseError.reason
        getOutputXmlTextBox.ForeColor = vbRed
        Exit Sub
    End If

    postTranslateJsonTextBox.Text = jsonText
    httpRequest.Send jsonText
    Dim output As String
    output = httpRequest.responseText
    postOutputJsonTextBox.Text = output
    statusCodeTextBox.Text = httpRequest.Status
    If httpRequest.Status <> 201 Then
        statusCodeTextBox.ForeColor = vbRed
    Else
        statusCodeTextBox.ForeColor = vbBlack
    End If
End Sub

Private Sub SendGetRequest(Optional ByVal customFirstName As String = "")
    Dim httpRequest As WinHttp.WinHttpRequest
    Set httpRequest = New WinHttp.WinHttpRequest
    Dim url As String
    url = "https://jsonplaceholder.typicode.com/todos/1"
    httpRequest.Open "GET", url
    httpRequest.Send
    Dim responseText As String
    responseText = httpRequest.responseText
    getOutputJsonTextBox.Text = responseText
    statusCodeTextBox.Text = ""
    Dim xmlString As String
    If Len(customFirstName) > 0 Then
        xmlString = CreateXmlWithCustomFirstName(customFirstName)
    Else
        xmlString = GenerateXmlFromJson(responseText)
    End If

    getOutputXmlTextBox.Text = xmlString
    statusCodeTextBox.Text = httpRequest.Status
    If httpRequest.Status <> 201 Then
        statusCodeTextBox.ForeColor = vbRed
    Else
        statusCodeTextBox.ForeColor = vbBlack
    End If
End Sub

Private Function CreateXmlWithCustomFirstName(ByVal customFirstName As String) As String
    Dim xmlDocument As DOMDocument60
    Set xmlDocument = New DOMDocument60
    Dim xmlElement As IXMLDOMElement
    Set xmlElement = xmlDocument.appendChild(xmlDocument.createElement("Myinfo"))
    xmlElement.appendChild(xmlDocument.createElement("FirstName")).Text = "My First Name"
    xmlElement.appendChild(xmlDocument.createElement("LastName")).Text = "My Last Name"
    xmlElement.appendChild(xmlDocument.createElement("StreetAdd")).Text = "My Address"

    If Len(customFirstName) > 0 Then
        Dim xmlNode As IXMLDOMNode
        Set xmlNode = xmlDocument.selectSingleNode("/Myinfo/FirstName")
        xmlNode.Text = customFirstName
    End If
    CreateXmlWithCustomFirstName = xmlDocument.xml
End Function

Private Function GenerateXmlFromJson(ByVal json As String) As String
    Dim xmlDocument As DOMDocument60
    Set xmlDocument = New DOMDocument60
    Dim xmlElement As IXMLDOMElement
    Set xmlElement = xmlDocument.appendChild(xmlDocument.createElement("xml"))

    Dim savedQuoteCharacterLocation As Integer
    Dim lastLoopHadAStartQuote As Boolean
    Dim lastLoopHadFullAttribute As Boolean
    Dim attributeName As String
    Do
        Dim searchCharacter As String
        If lastLoopHadFullAttribute Then
            searchCharacter = ","
        Else
            searchCharacter = """"
        End If
        Dim currentQuoteCharacterLocation As Integer
        currentQuoteCharacterLocation = InStr(savedQuoteCharacterLocation + 1, json, searchCharacter, vbTextCompare)
        If currentQuoteCharacterLocation = 0 And lastLoopHadFullAttribute Then
            searchCharacter = "}"
            currentQuoteCharacterLocation = InStr(savedQuoteCharacterLocation + 1, json, searchCharacter, vbTextCompare)
        End If
        If currentQuoteCharacterLocation = 0 Then
            Exit Do
        Else
            If lastLoopHadFullAttribute Then
                Dim currentValue As String
                currentValue = Replace(Replace(Trim(Mid(json, savedQuoteCharacterLocation + 2, currentQuoteCharacterLocation - savedQuoteCharacterLocation - 2)), vbCr, ""), vbLf, "")
                If Len(currentValue) >= 2 And Left(currentValue, 1) = """" And Right(currentValue, 1) = """" Then
                    currentValue = Mid(currentValue, 2, Len(currentValue) - 2)
                End If

                xmlElement.appendChild(xmlDocument.createElement(attributeName)).Text = currentValue

                lastLoopHadFullAttribute = False
            Else
                If lastLoopHadAStartQuote Then
                    attributeName = Mid(json, savedQuoteCharacterLocation + 1, currentQuoteCharacterLocation - savedQuoteCharacterLocation - 1)

                    lastLoopHadAStartQuote = False
                    lastLoopHadFullAttribute = True
                Else
                    lastLoopHadAStartQuote = True
                End If
            End If
        End If
        savedQuoteCharacterLocation = currentQuoteCharacterLocation
    Loop
    GenerateXmlFromJson xmlDocument.xml
End Function
