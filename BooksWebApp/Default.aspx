<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BooksWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Books</h1>
    </div>

    <h2>Available books</h2>
    <div class="form-inline">
        <asp:TextBox ID="searchTxtBox" runat="server" CssClass="form-control" Style="margin-bottom: 5px;" placeholder="Search ..."></asp:TextBox>
        <asp:DropDownList CssClass="form-control" Style="width: inherit; margin-bottom: 5px;" ID="criteriaDDL" runat="server">
            <asp:ListItem Text="-- Search by --" Value="" />
            <asp:ListItem>ISBN</asp:ListItem>
            <asp:ListItem>Title</asp:ListItem>
            <asp:ListItem>Author</asp:ListItem>
            <asp:ListItem>Publisher</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList CssClass="form-control" Style="width: inherit; margin-bottom: 5px;" ID="catDDL" runat="server">
            <asp:ListItem Text="-- Select category --" Value="" />
        </asp:DropDownList>
        <asp:Button ID="searchButton" runat="server" Text="Search" CssClass="btn btn-success" Style="margin-bottom: 5px;" OnClick="searchButton_Click" />
    </div>

    <div class="table-responsive">
        <asp:Repeater ID="booksRepeater" runat="server">
            <HeaderTemplate>
                <table class="table table-hover table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>ISBN</th>
                            <th>Title</th>
                            <th>Author</th>
                            <th>Publisher</th>
                            <th>Category</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("ISBN") %></td>
                    <td><%# Eval("Title") %></td>
                    <td><%# string.Concat(Eval("Author.firstName")," ",Eval("Author.lastName")) %></td>
                    <td><%# Eval("Publisher.publisherName") %></td>
                    <td><%# Eval("Category.categoryName") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <h3>Something is missing?</h3>
    <small><em><a href="javascript:void(0);" data-bind="click: shouldShowForm.toggle">Add new book to our database</a></em></small>
    <div class="form-inline" data-bind="visible: shouldShowForm">
        <asp:Label ID="isbnLabel" AssociatedControlID="isbnTxtBox" CssClass="sr-only" runat="server" Text="ISBN"></asp:Label>
        <asp:TextBox ID="isbnTxtBox" runat="server" CssClass="form-control" placeholder="ISBN" Style="margin-bottom: 5px;" ValidationGroup="A"></asp:TextBox>
        <asp:RequiredFieldValidator ID="isbnRequiredField" runat="server" ErrorMessage="ISBN is required!" ControlToValidate="isbnTxtBox" ForeColor="Red" ValidationGroup="A" >*</asp:RequiredFieldValidator>
        <asp:Label ID="titleLabel" AssociatedControlID="titleTxtBox" CssClass="sr-only" runat="server" Text="Title"></asp:Label>
        <asp:TextBox ID="titleTxtBox" runat="server" CssClass="form-control" placeholder="Title" Style="margin-bottom: 5px;" ValidationGroup="A"></asp:TextBox>
        <asp:RequiredFieldValidator ID="titleRequiredField" runat="server" ErrorMessage="Title is required!" ControlToValidate="titleTxtBox" ForeColor="Red" ValidationGroup="A">*</asp:RequiredFieldValidator>
        <asp:Label ID="authorLabel" AssociatedControlID="authorTxtBox" CssClass="sr-only" runat="server" Text="Author"></asp:Label>
        <asp:TextBox ID="authorTxtBox" runat="server" CssClass="form-control" placeholder="Author" Style="margin-bottom: 5px;" ValidationGroup="A" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="authorRequiredField" runat="server" ErrorMessage="Author is required!" ControlToValidate="authorTxtBox" ForeColor="Red" ValidationGroup="A" >*</asp:RequiredFieldValidator>
        <asp:Label ID="publisherLabel" AssociatedControlID="publisherTxtBox" CssClass="sr-only" runat="server" Text="Publisher"></asp:Label>
        <asp:TextBox ID="publisherTxtBox" runat="server" CssClass="form-control" placeholder="Publisher" Style="margin-bottom: 5px;" ValidationGroup="A"></asp:TextBox>
        <asp:RequiredFieldValidator ID="publisherRequiredField" runat="server" ErrorMessage="Publisher is required!" ControlToValidate="publisherTxtBox" ForeColor="Red" ValidationGroup="A" >*</asp:RequiredFieldValidator>
        <asp:Label ID="categoryLabel" AssociatedControlID="categoryTxtBox" CssClass="sr-only" runat="server" Text="Category"></asp:Label>
        <asp:TextBox ID="categoryTxtBox" runat="server" CssClass="form-control" placeholder="Category" Style="margin-bottom: 5px;" ValidationGroup="A"></asp:TextBox>
        <asp:RequiredFieldValidator ID="categoryRequiredField" runat="server" ErrorMessage="Category is required!" ControlToValidate="categoryTxtBox" ForeColor="Red" ValidationGroup="A" >*</asp:RequiredFieldValidator>
        <asp:Button ID="newBookButton" runat="server" Text="Submit"  CssClass="btn btn-primary" Style="margin-bottom: 5px;" OnClick="newBookButton_Click" ValidationGroup="A"/>
    </div>

    <div id="validationSummary">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="A" />
    </div>
    <br />

    <div class="form-inline">
        <button class="btn btn-primary" onclick="getBooks(); return false;">Get Books</button>
        <button class="btn btn-danger" onclick="getAuthors(); return false;">Get Authors</button>
        <button class="btn btn-success" onclick="getPublishers(); return false;">Get Publishers</button>
        <button class="btn btn-warning" onclick="getCategories(); return false;">Get Categories</button>
    </div>
    <br />

    <div>
        <pre id="result"></pre>
    </div>

    <script type="text/javascript">
        //ko start
        function ViewModel() {
            var self = this;

            ko.observable.fn.toggleable = function () {
                var self = this;
                self.toggle = function () {
                    self(!self());
                };
                return self;
            };

            self.shouldShowForm = ko.observable(false).toggleable();
        }
        ko.applyBindings(new ViewModel());
        //ko end

        //addAuthor not in use!
        function addAuthor() {
            var names = {
                "firstName": $('#firstName').val(),
                "lastName": $('#lastName').val()
            };

            $.ajax({
                url: "Service/BooksService.svc/AddAuthor",
                type: "POST",
                data: JSON.stringify(names),
                dataType: "json",
                contentType: "application/json",
                success: function (result) {
                    console.info(result);
                },
                error: function (error) {
                    console.error(error);
                }
            });
        }

        function getBooks() {
            $.ajax({
                url: "Service/BooksService.svc/Books",
                type: "GET",
                dataType: "json",
                success: function (result) {
                    $('#result').text(JSON.stringify(result, null, 2));
                }
            });
        }

        function getAuthors() {
            $.ajax({
                url: "Service/BooksService.svc/Authors",
                type: "GET",
                dataType: "json",
                success: function (result) {
                    $('#result').text(JSON.stringify(result, null, 2));
                }
            });
        }

        function getCategories() {
            $.ajax({
                url: "Service/BooksService.svc/Categories",
                type: "GET",
                dataType: "json",
                success: function (result) {
                    $('#result').text(JSON.stringify(result, null, 2));
                }
            });
        }

        function getPublishers() {
            $.ajax({
                url: "Service/BooksService.svc/Publishers",
                type: "GET",
                dataType: "json",
                success: function (result) {
                    $('#result').text(JSON.stringify(result, null, 2));
                }
            });
        }
    </script>
</asp:Content>
