(function(){
    angular.module("main",["replicationServiceModule"])
        .controller("replicationCtr",[
            "$scope","replicationService",
            function($scope, service){

              $scope.getUser= function(id){
                service.getUser(id).then(
                    function(){
                        alert('GOOD');
                    },
                    function(){
                        alert('ERROR')
                    }
                );
              }
            }]);
}())
