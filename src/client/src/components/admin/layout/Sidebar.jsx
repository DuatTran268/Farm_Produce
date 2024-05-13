import React, { useState } from "react";
import {
  faArrowCircleUp,
  faCar,
  faDashboard,
  faEdit,
  faList,
  faMessage,
  faSignOut,
  faTicket,
  faUpDown,
  faUser,
  faUserCircle,
} from "@fortawesome/free-solid-svg-icons";
import SidebarCommon from "../common/SidebarCommon";
import { faImage } from "@fortawesome/free-regular-svg-icons";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Sidebar = () => {
  const [activeMenu, setActiveMenu] = useState();
  const handleMenuClick = (slug) => {
    setActiveMenu(slug);
  };

  return (
    <>
      <div className="sidebar">
        <ul>
          <div className="sidebar-main">
            <span className="sidebar-tile">Bảng điều khiển</span>
            <SidebarCommon
              slug="dashboard"
              icon={faDashboard}
              title="Dashboard"
            />
          </div>

          <div className="sidebar-main">
            <span className="sidebar-tile">Quản lý</span>
            <SidebarCommon slug="product" icon={faTicket} title="Sản phẩm" />
            <SidebarCommon slug="category" icon={faList} title="Danh mục" />
            <SidebarCommon
              slug="order"
              icon={faCar}
              title="Đơn hàng"
            />
            {/* <SidebarCommon slug="deliver" icon={faCar} title="Vận chuyển" /> */}
            <SidebarCommon slug="comment" icon={faMessage} title="Bình luận" />
            <SidebarCommon slug="user" icon={faUser} title="Người dùng" />
            <SidebarCommon slug="image" icon={faImage} title="Ảnh sản phẩm" />
            <SidebarCommon slug="unit" icon={faList} title="Đơn vị" />

            <SidebarCommon slug="discount" icon={faUpDown} title="Giảm giá" />
          </div>

          <div className="sidebar-main">
            {/* <span className="sidebar-tile">Cài đặt</span>
            <SidebarCommon slug="setting" icon={faEdit} title="Cài đặt" /> */}
            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/login`}>
                <li>
                  <FontAwesomeIcon icon={faSignOut} />
                  <span className="px-3">Đăng xuất</span>
                </li>
              </Link>
            </div>
          </div>
        </ul>
      </div>
    </>
  );
};
export default Sidebar;
