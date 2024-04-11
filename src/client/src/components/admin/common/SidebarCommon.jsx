import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useEffect } from "react";
import { Link } from "react-router-dom";

const SidebarCommon = ({ slug, icon, title, active, onClick }) => {



  return (
    <div className="sidebar-wrapper">
      <Link className="sidebar-link" to={`/admin/${slug}`}>
        <li className={active ? "active" : ""} onClick={() => onClick(slug)}>
          <FontAwesomeIcon icon={icon} />
          <span className="px-3">{title}</span>

          <style jsx>{`
        .active {
          color: red;
          font-weight: 700;
      `}</style>
        </li>
      </Link>
    </div>
  );
};
export default SidebarCommon;
