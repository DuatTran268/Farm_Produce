// Admin.js
import * as React from "react";
import { Admin, Resource, ListGuesser } from "react-admin";
import { dataProvider } from "../../apiProvider";

const AdminPanel = () => (
  <Admin dataProvider={dataProvider}>
    <Resource name="account" list={ListGuesser} />
  </Admin>
);

export default AdminPanel;
