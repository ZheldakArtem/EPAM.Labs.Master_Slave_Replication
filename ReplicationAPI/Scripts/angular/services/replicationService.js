(function () {
    angular.module('replicationServiceModule', []).
        service('replicationService',['$http', function ($http) {
            return {
                getUser: function (id) {
                    return $http.get('../Home/GetUser/' + id);
                }
                //deleteUser: function (user) {
                //    return $http.delete(urlSystem + 'DeleteFolder?path=' + path);
                //},
                //createUser: function (data) {
                //    return $http.post(urlSystem + 'CreateFolder', data);
                //}
            }
        }]);
}());