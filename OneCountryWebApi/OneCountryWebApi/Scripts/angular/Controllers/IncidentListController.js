var IncidentListController = function ($scope, $location, GetFactory, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    $scope.location = '';
    $scope.Incidents = Array();

    $scope.loadTeamDetails = function () {
        if ($scope.userID != 0) {
            var obj = {
                UserID: $scope.userID,
                location: $scope.location
            };
            var url = '/api/TeamAPI/GetTeamDetails';
            var result = PostFactory(url, obj);
            result.then(function (result) {
                if (result.success) {
                    $scope.Team = result.data;
                    $scope.TeamCount = result.data.length;
                    console.log(result.data);
                } else {
                    ShowMessage('danger', 'Error occured while processing.');
                }
            });
        }
    }
}

IncidentListController.$inject = ['$scope', '$location', 'GetFactory', 'PostFactory']