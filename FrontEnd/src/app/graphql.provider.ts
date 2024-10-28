import { Apollo, APOLLO_NAMED_OPTIONS, APOLLO_OPTIONS, NamedOptions } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { ApplicationConfig, inject } from '@angular/core';
import { ApolloClientOptions, InMemoryCache } from '@apollo/client/core';
import { enviroments } from '../environments/enviroments';


const equipmentUri =enviroments.equipmentService;
const userUri = enviroments.userService;
const apiGateWay = enviroments.apiGateway;





export const graphqlProvider: ApplicationConfig['providers'] = [
  Apollo,
  {
    provide: APOLLO_NAMED_OPTIONS, // <-- Different from standard initialization
    useFactory(httpLink: HttpLink): NamedOptions {
      return {
        apiGateWay: /* <-- These settings will be saved by name: apiGateWay */ {
          cache: new InMemoryCache(),
          link: httpLink.create({
            uri: apiGateWay,
          }),
        },
        userService: /* <-- These settings will be saved by name: newClientName */ {
          cache: new InMemoryCache(),
          link: httpLink.create({
            uri: userUri,
          }),
        },
        equipmentService: /* <-- This settings will be saved as default client */ {
          cache: new InMemoryCache(),
          link: httpLink.create({
            uri: equipmentUri,
          }),
        },
        
      };
    },
    deps: [HttpLink],
  },
];
