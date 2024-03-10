import React from "react";
import {
  faArrowCircleUp,
  faBook,
  faDashboard,
  faEdit,
  faHomeAlt,
  faMessage,
  faPen,
  faSignOut,
  faUser,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";


const Sidebar = () => {
  return (
    <>
    <div className="sidebar">
        <ul>
          <div className="sidebar-main">
            <span className="sidebar-tile">Bảng điều khiển</span>
            <Link className="sidebar-link" to={`/admin/dashboard`}>
              <li>
                <FontAwesomeIcon icon={faDashboard} />
                <span className="px-3">Dashboard</span>
              </li>
            </Link>
          </div>

          <div className="sidebar-main">
            <span className="sidebar-tile">Quản lý</span>
            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/admin/user`}>
                <li>
                  <FontAwesomeIcon icon={faUser} />
                  <span className="px-3">Người dùng</span>
                </li>
              </Link>
            </div>
            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/admin/product`}>
                <li>
                  <FontAwesomeIcon icon={faBook} />
                  <span className="px-3">Sản phẩm</span>
                </li>
              </Link>
            </div>

            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/admin/category`}>
                <li>
                  <FontAwesomeIcon icon={faPen} />
                  <span className="px-3">Danh mục</span>
                </li>
              </Link>
            </div>

            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/admin/order`}>
                <li>
                  <FontAwesomeIcon icon={faHomeAlt} />
                  <span className="px-3">Đơn hàng</span>
                </li>
              </Link>
            </div>
            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/admin/transport`}>
                <li>
                  <FontAwesomeIcon icon={faArrowCircleUp} />
                  <span className="px-3">Vận chuyển</span>
                </li>
              </Link>
            </div>
            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/admin/comment`}>
                <li>
                  <FontAwesomeIcon icon={faMessage} />
                  <span className="px-3">Bình luận</span>
                </li>
              </Link>
            </div>
          </div>

          <div className="sidebar-main">
            <span className="sidebar-tile">Cài đặt</span>
            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/`}>
                <li>
                  <FontAwesomeIcon icon={faEdit} />
                  <span className="px-3">Tuỳ chỉnh</span>
                </li>
              </Link>
            </div>
            <div className="sidebar-wrapper">
              <Link className="sidebar-link" to={`/`}>
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
  )
}
export default Sidebar;