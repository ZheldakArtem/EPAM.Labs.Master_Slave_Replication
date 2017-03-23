(function () {
    angular.module('replicationServiceModule', []).
        service('replicationService',['$http', function ($http) {
            return {
                getUserMaster: function (id) {
                    return $http.get('../Home/GetUserMaster/' + id);
                },
                getUserSlaves: function (id) {
                	return $http.get('../Home/GetUserSlaves/' + id);
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