import React from "react";
import Sidebar from "../layout/Sidebar";
import Navbar from "../layout/Navbar";
import "../../../styles/admin/layout.scss"

const LayoutCommon = ({ children }) => {
  return (
    <>
      <div className="">
        <Navbar />
        <div className="layout_wrapper">
          <div className="col-2">
            <Sidebar />
          </div>
          <div className="col-10 layout_admin">
            {children}
          </div>
        </div>
      </div>
    </>
  );
};
export default LayoutCommon;
