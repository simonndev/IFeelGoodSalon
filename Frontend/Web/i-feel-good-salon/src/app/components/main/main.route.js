let routeConfig = ($stateProvider, $urlRouterProvider) => {
  'ngInject';

  $stateProvider
    .state('main', {
      url: '/',
      template: '<main></main>'
    });

  $urlRouterProvider.otherwise('/');
};

export default routeConfig;
