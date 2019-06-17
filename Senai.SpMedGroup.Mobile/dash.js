import React, { Component } from 'react';
import { StyleSheet, Text, View, SafeAreaView, ScrollView, Dimensions, Image } from 'react-native';
import { createAppContainer, createDrawerNavigator, DrawerItems } from 'react-navigation';

import App from './App';
import Main from './src/pages/Main';

const { width } = Dimensions.get('window')

export default class App extends Component {
    render() {
        return (
            <AppContainer />
        );
    }
}

const CustomDrawerComponent = (props) => (
    <SafeAreaView style={{flex:1}}>
        <View style={{height: 150, backgroundColor: 'white', alignItems: 'center', justifyContent: 'center'}}>
            <Image style={{height:100, width: 100, borderRadius: 60}}/>
        </View>
        <ScrollView>
            <DrawerItems {...props}/>
        </ScrollView>
    </SafeAreaView>
)

const AppDrawerNavigator = createDrawerNavigator({
    Main: Main,
    App: App,
},{
    initialRouteName: 'App',
    drawerWidth: 0.65 * width,
    contentComponent: CustomDrawerComponent,
    contentOptions: {
        activeTintColor: 'orange'
    },
}
)

const AppContainer = createAppContainer(AppDrawerNavigator);

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
        alignItems: 'center',
        justifyContent: 'center',
    },
});