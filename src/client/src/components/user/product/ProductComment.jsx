import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCommentBySlugOfProduct } from "../../../api/Comment";


const ProductComment = () => {

  const [comment, setComment] = useState([]);
  const params = useParams();
  const {slug} = params;

  useEffect(() => {
    getCommentBySlugOfProduct(slug).then((data) => {
      if(data){
        setComment(data.items);
      }
      else{
        setComment({});
      }
    });
  }, [slug]);



  return(
    <section>
      <div className="comment_content">
        <div className="comment_title">Bình luận đã để lại</div>
      </div>
      <div className="comment_wrapper">
        {comment.length > 0 ? (
          comment.map((item, index) => (
            <>
              <div className="comment_item" key={index}>
              <h1>Comment{item.name}</h1>
              <h4>Content {item.commentText}</h4>
              </div>
            </>
          ))
        ) : (
          <>Khong có comment</>
        )};
      </div>
      
    </section>
  )
}
export default ProductComment;