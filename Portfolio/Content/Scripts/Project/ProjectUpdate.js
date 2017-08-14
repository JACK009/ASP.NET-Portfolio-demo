var ProjectApp = angular.module('ProjectUpdate', [])
    .controller('ProjectUpdateTagController', [
        '$scope',
        '$location',
        'TagService',
        function ($scope, $location, TagService) {
            var url = $location.absUrl();
            var projectId = url.substring(url.indexOf("Edit/") + "Edit/".length);
            $scope.tags = [];
            $scope.newTag = '';
            $scope.tagValidationMessage = '';

            TagService.GetAssignedTagsByProjectId(projectId).then(function (result) {
                $scope.tags = result;
            });

            $scope.validTag = function (checkEmpty = true, clearValidationMessage = true) {
                var isValid = true;
                if (clearValidationMessage === true) {
                    $scope.tagValidationMessage = '';
                }

                if ($scope.newTag.length === 0 && checkEmpty) {
                    $scope.tagValidationMessage = 'Het veld "Tags" mag niet leeg zijn.';

                    isValid = false;
                }

                angular.forEach($scope.tags, function (value, key) {
                    if (value.Name.toLowerCase() === $scope.newTag.toLowerCase()) {
                        $scope.tagValidationMessage = 'Het veld "Tags" moet uniek zijn.';

                        isValid = false;
                    }
                });

                return isValid;
            };
            
            $scope.addTag = function () {
                if ($scope.validTag(true, false) === true) {
                    var newTag = {
                        'newTag': $scope.newTag
                    };

                    TagService.PostTag(newTag, projectId).then(function (result) {
                        $scope.tags = result;
                        $scope.newTag = '';
                    });
                }
            };
     }])
    .controller('ProjectUpdateCategoryController', [
        '$scope',
        '$location',
        'ProjectCategoryService',
        function ($scope, $location, ProjectCategoryService) {
            var url = $location.absUrl();
            var projectId = url.substring(url.indexOf("Edit/") + "Edit/".length);
            $scope.projectCategories = [];
            $scope.newProjectCategory = '';
            $scope.projectCategoryValidationMessage = '';

            ProjectCategoryService.GetAssignedProjectCategoriesByProjectId(projectId).then(function (result) {
                $scope.projectCategories = result;
            });

            $scope.validProjectCategory = function (checkEmpty = true, clearValidationMessage = true) {
                var isValid = true;
                if (clearValidationMessage === true) {
                    $scope.projectCategoryValidationMessage = '';
                }

                if ($scope.newProjectCategory.length === 0 && checkEmpty) {
                    $scope.projectCategoryValidationMessage = 'Het veld "Categories" mag niet leeg zijn.';

                    isValid = false;
                }

                angular.forEach($scope.projectCategories, function (value, key) {
                    if (value.Name.toLowerCase() === $scope.newProjectCategory.toLowerCase()) {
                        $scope.projectCategoryValidationMessage = 'Het veld "Categories" moet uniek zijn.';

                        isValid = false;
                    }
                });

                return isValid;
            };

            $scope.addProjectCategory = function () {
                if ($scope.validProjectCategory(true, false) === true) {
                    var newProjectCategory = {
                        'newProjectCategory': $scope.newProjectCategory
                    };

                    ProjectCategoryService.PostProjectCategory(newProjectCategory, projectId).then(function (result) {
                        $scope.projectCategories = result;
                        $scope.newProjectCategory = '';
                    });
                }
            };
    }])
    .service('TagService', ['TagFactory', '$q', function (TagFactory, $q) {
        var self = this;

        self.PostTag = function (tag, projectId) {
            var deferred = $q.defer();

            TagFactory.PostTag(tag)
                .then(
                function () {
                    self.GetAssignedTagsByProjectId(projectId).then(function (result) {
                        deferred.resolve(result);
                    });
                });

            return deferred.promise;
        };

        self.GetAssignedTagsByProjectId = function(projectId) {
            var tags = [];
            var deferred = $q.defer();
            TagFactory.GetAssignedProjectModelRelatedTags(projectId).then(
                function (response) {
                    angular.forEach(response.data, function (retreivedTag, key) {
                        var tag = {
                            'Name': retreivedTag.Name,
                            'Assigned': retreivedTag.Assigned,
                            'Id': retreivedTag.Id
                        };
                        tags.push(tag);
                    });
                    deferred.resolve(tags);
                });

            return deferred.promise;
        };
    }])
    .service('ProjectCategoryService', ['ProjectCategoryFactory', '$q', function (ProjectCategoryFactory, $q) {
        var self = this;

        self.PostProjectCategory = function (projectCategory, projectId) {
            var deferred = $q.defer();

            ProjectCategoryFactory.PostProjectCategory(projectCategory)
                .then(
                function () {
                    self.GetAssignedProjectCategoriesByProjectId(projectId).then(function (result) {
                        deferred.resolve(result);
                    });
                });

            return deferred.promise;
        };

        self.GetAssignedProjectCategoriesByProjectId = function(projectId) {
            var projectCategories = [];
            var deferred = $q.defer();

            ProjectCategoryFactory.GetAssignedProjectModelRelatedProjectCategories(projectId).then(
                function (response) {
                    angular.forEach(response.data, function (retreivedProjectCategory, key) {
                        var projectCategory = {
                            'Name': retreivedProjectCategory.Name,
                            'Assigned': retreivedProjectCategory.Assigned,
                            'Id': retreivedProjectCategory.Id
                        };
                        projectCategories.push(projectCategory);
                    });
                    deferred.resolve(projectCategories);
                });

            return deferred.promise;
        };
    }])
    .factory('ProjectCategoryFactory', ['$http', function ($http) {
        var factory = {};
        factory.GetAssignedProjectModelRelatedProjectCategories = function (projectId) {
            return $http({
                url: '/ProjectCategory/GetAssignedProjectModelRelatedProjectCategories',
                method: 'GET',
                params: { 'projectId': projectId }
            });
        };
        factory.PostProjectCategory = function (projectCategory) {
            return $http.post('/ProjectCategory/PostProjectCategory', projectCategory);
        };

        return factory;
    }])
    .factory('TagFactory', ['$http', function ($http) {
        var factory = {};
        factory.GetAssignedProjectModelRelatedTags = function (projectId) {
            return $http({
                url: '/Tag/GetAssignedProjectModelRelatedTags',
                method: 'GET',
                params: { 'projectId': String(projectId) }
            });
        };
        factory.PostTag = function (projectCategory) {
            return $http.post('/Tag/PostTag', projectCategory);
        };

        return factory;
    }]);

