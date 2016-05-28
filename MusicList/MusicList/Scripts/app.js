var ViewModel = function () {
    var self = this;
    self.songs = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.artists = ko.observableArray();
    self.newSong = {
        Artist: ko.observable(),
        Type: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable(),
        Year: ko.observable()
    }

    var songsUri = '/api/Songs/';
    var artistsUri = '/api/Artists/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllSongs() {
        ajaxHelper(songsUri, 'GET').done(function (data) {
            self.songs(data);
        });
    }

    self.deletesong = function (item) {
        ajaxHelper(songsUri + item.Id, 'DELETE').done(function (data) {
            self.songs.remove(item);
        });
    }

  

    self.getSongDetail = function (item) {
        ajaxHelper(songsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    function getArtists() {
        ajaxHelper(artistsUri, 'GET').done(function (data) {
            self.artists(data);
        });
    }


    self.addSong = function (formElement) {
        var song = {
            ArtistId: self.newSong.Artist().Id,
            Type: self.newSong.Type(),
            Price: self.newSong.Price(),
            Title: self.newSong.Title(),
            Year: self.newSong.Year()
        };

        ajaxHelper(songsUri, 'POST', song).done(function (item) {
            self.songs.push(item);
        });

        
    };

    
    // Fetch the initial data.
    getAllSongs();
    getArtists();
};

ko.applyBindings(new ViewModel());