(function () {
	angular.module("main", ["replicationServiceModule"])
        .controller("replicationCtr", [
            "$scope", "$window", "replicationService",
            function ($scope, $window, service) {
            	var startTime, endTime, counter = 0;
            	var index1 = makeCounter();
            	var index2 = makeCounter();
            	$window.numbReq = [500, 1000, 1500, 2000, 3000, 4000, 6000, 6500];
            	$window.masterData = [];
            	$window.slavesData = [];

            	$scope.getUserMaster = function () {
            		var ind = index1.getNext();
            		startTime = new Date().getTime();

            		for (var i = 0; i < $window.numbReq[ind]; i++) {
            			testGetUserMaster(10, $window.numbReq[ind]);
            		}
            	}

            	$scope.getUserSlaves = function () {
            		var ind = index2.getNext();
            		startTime = new Date().getTime();

            		for (var i = 0; i < $window.numbReq[ind]; i++) {
            			testGetUserSlaves(10, $window.numbReq[ind]);
            		}
            	}

            	function testGetUserMaster(id, max) {
            		service.getUserMaster(id).then(
				   function () {
				   	counter++;
				   	if (counter === max) {
				   		endTime = new Date().getTime() - startTime;
				   		$window.masterData.push(endTime);
				   		counter = 0;
				   		if ($window.masterData.length < 10) {
							   $scope.getUserMaster();
						   }
				   	}
				   },
				   function () {
				   	alert('ERROR');
				   }
			   );
            	}

            	function testGetUserSlaves(id, max) {
            		service.getUserSlaves(id).then(
				   function () {
				   	counter++;
				   	if (counter === max) {
				   		endTime = new Date().getTime() - startTime;
				   		$window.slavesData.push(endTime);
				   		counter = 0;
				   		if ($window.slavesData.length < 10) {
				   			$scope.getUserSlaves();
				   		}
				   	}
				   },
				   function () {
				   	alert('ERROR');
				   }
			   );
            	}
            	function makeCounter() {
            		var currentCount = 0;
            		return {
            			getNext: function () {
            				return currentCount++;
            			},
            			reset: function () {
            				currentCount = 0;
            			}
            		};
            	}
            }]);
}())
