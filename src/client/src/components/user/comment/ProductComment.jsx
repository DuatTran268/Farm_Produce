import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCommentBySlugOfProduct } from "../../../api/Comment";
import "../../user/product/ProductDetail.css";
import { format } from "date-fns";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser,  faStar } from "@fortawesome/free-solid-svg-icons";
import { faStar as faStarRegular } from "@fortawesome/free-regular-svg-icons";
const ProductComment = () => {
  const [comment, setComment] = useState([]);
  const params = useParams();
  const { slug } = params;

  useEffect(() => {
    getCommentBySlugOfProduct(slug).then((data) => {
      if (data) {
        setComment(data.items);
      } else {
        setComment({});
      }
    });
  }, [slug]);

    // Function to generate star icons based on rating
    const renderRatingStars = (rating) => {
      const stars = [];
      for (let i = 0; i < 5; i++) {
        if (i < rating) {
          stars.push(  <FontAwesomeIcon key={i} icon={faStar} className="text-warning"/>);
        } else {
          stars.push(<FontAwesomeIcon key={i} icon={faStarRegular} className="text-warning"/>);
        }
      }
      return stars;
    };

  return (
    <section className="comment_container">
      <div className="comment_content">
        <h5 className="comment_title">Bình luận đã để lại</h5>
      </div>
      <div className="comment_wrapper">
        {comment.length > 0 ? (
          comment.map((item, index) => (
            <div className="comment_item " key={index}>
              <div className="comment_item_col col-2">
                <FontAwesomeIcon icon={faUser} fontSize={60} />
              </div>
              <div className="comment_item_col col-10">
                <div className="comment_item_col_top row">
                  <h5 className="col-6">Tên: {item.name}</h5>
                  <div className="col-3">{renderRatingStars(item.rating)}</div>
                  <div className="col-3">
                    {format(new Date(item.created), "dd/MM/yyyy")}
                  </div>
                </div>
                <div className="comment_item_col_bottom">
                  Nội dung: {item.commentText}
                </div>
              </div>
            </div>
          ))
        ) : (
          <p>Chưa có bình luận nào</p>
        )}
      </div>
    </section>
  );
};
export default ProductComment;
