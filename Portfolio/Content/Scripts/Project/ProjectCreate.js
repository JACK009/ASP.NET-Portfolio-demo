var ProjectApp = angular.module('ProjectCreate', [])
    .controller('ProjectCreateTagController', [
        '$scope',
        'TagService',
        function ($scope, TagService) {
            $scope.tags = [];
            $scope.newTag = '';
            $scope.tagValidationMessage = '';

            TagService.GetTags().then(function (result) {
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
                    if (value.name.toLowerCase() === $scope.newTag.toLowerCase()) {
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

                    TagService.PostTag(newTag).then(function (result) {
                        $scope.tags = result;
                        $scope.newTag = '';
                    });
                }
            };
    }])
    .controller('ProjectCreateCategoryController', [
        '$scope',
        'ProjectCategoryService',
        function ($scope, ProjectCategoryService) {
            $scope.projectCategories = [];
            $scope.newProjectCategory = '';
            $scope.projectCategoryValidationMessage = '';
            
            ProjectCategoryService.GetProjectCategories().then(function (result) {
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
                    if (value.name.toLowerCase() === $scope.newProjectCategory.toLowerCase()) {
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

                    ProjectCategoryService.PostProjectCategory(newProjectCategory).then(function (result) {
                        $scope.projectCategories = result;
                        $scope.newProjectCategory = '';
                    });
                }
            };
        }])
    .service('TagService', ['TagFactory', '$q', function (TagFactory, $q) {
        var self = this;

        self.PostTag = function (tag) {
            var deferred = $q.defer();

            TagFactory.PostTag(tag)
                .then(
                function () {
                    self.GetTags().then(function (result) {
                        deferred.resolve(result);
                    });
                });

            return deferred.promise;
        };
        self.GetTags = function () {
            var tags = [];
            var deferred = $q.defer();

            TagFactory.GetTags().then(
                function (response) {
                    angular.forEach(response.data, function (retreivedTag, key) {
                        var tag = {
                            'name': retreivedTag.Name,
                            'id': retreivedTag.Id
                        };
                        tags.push(tag);
                    });
                    deferred.resolve(tags);
                });

            return deferred.promise;
        };
    }])
    .service('ProjectCategoryService', ['ProjectCategoryFactory', '$q', function (ProjectFactory, $q) {
        var self = this;

        self.PostProjectCategory = function (projectCategory) {
            var deferred = $q.defer();

            ProjectFactory.PostProjectCategory(projectCategory)
                .then(
                function () {
                    self.GetProjectCategories().then(function (result) {
                        deferred.resolve(result);
                    });
                });

            return deferred.promise;
        };
        self.GetProjectCategories = function () {
            var projectCategories = [];
            var deferred = $q.defer();

            ProjectFactory.GetProjectCategories().then(
                function (response) {
                    angular.forEach(response.data, function (retreivedProjectCategory, key) {
                        var projectCategory = {
                            'name': retreivedProjectCategory.Name,
                            'id': retreivedProjectCategory.Id
                        };
                        projectCategories.push(projectCategory);
                    });
                    deferred.resolve(projectCategories);
                });

            return deferred.promise;
        };
    }])
    .factory('TagFactory', ['$http', function ($http) {
        var factory = {};
        factory.GetTags = function () {
            return $http.get('/Tag/GetTags');
        };
        factory.PostTag = function (tag) {
            return $http.post('/Tag/PostTag', tag);
        };

        return factory;
    }])
    .factory('ProjectCategoryFactory', ['$http', function ($http) {
        var factory = {};
        factory.GetProjectCategories = function () {
            return $http.get('/ProjectCategory/GetProjectCategories');
        };
        factory.PostProjectCategory = function (projectCategory) {
            return $http.post('/ProjectCategory/PostProjectCategory', projectCategory);
        };

        return factory;
    }]);